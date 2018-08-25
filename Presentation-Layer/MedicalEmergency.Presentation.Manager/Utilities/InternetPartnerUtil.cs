using LinqToExcel;
using MedicalEmergency.Domain.Entities;
using MedicalEmergency.Domain.Interfaces.Repositories;
using MedicalEmergency.Presentation.Manager.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace MedicalEmergency.Presentation.Manager.Utilities
{
    public static class InternetPartnerUtil
    {
        public static void ExecuteSync(IInternetPartnerRepository internetPartnerRepository, IInternetPartnerLocationRepository internetPartnerLocationRepository)
        {
            var list = internetPartnerRepository.GetAll();

            var file = new ExcelQueryFactory("C:\\Users\\eduardo.fernandes\\Downloads\\CORRESPONDENTES_25 01 18 (envio Otávio).xlsx");

            var fileList = file.Worksheet<InternetPartnerImportViewModel>("Plan1");

            foreach (InternetPartnerImportViewModel item in fileList)
            {
                var internetPartner = list.Where(x => x.StoreKeeperID == item.StoreKeeperID && x.StoreID == item.StoreID).FirstOrDefault();

                if (internetPartner != null)
                {
                    internetPartner.Name = item.Name;
                    internetPartner.Logradouro = item.Logradouro;
                    internetPartner.Abbreviation = item.Name;
                    internetPartner.City = item.City;
                    internetPartner.CNPJ = item.CNPJ;
                    internetPartner.Complement = item.Complement;
                    internetPartner.Neighborhood = item.Neighborhood;
                    internetPartner.Number = item.Number;
                    internetPartner.Phone = item.Phone;
                    internetPartner.State = item.State;
                    internetPartner.Update = DateTime.Now;
                    internetPartner.Zipcode = item.Zipcode;

                    internetPartnerRepository.Update(internetPartner);
                }
                else
                {
                    internetPartner = new InternetPartner();

                    internetPartner.Name = item.Name;
                    internetPartner.Logradouro = item.Logradouro;
                    internetPartner.Abbreviation = item.Name;
                    internetPartner.City = item.City;
                    internetPartner.CNPJ = item.CNPJ;
                    internetPartner.Complement = item.Complement;
                    internetPartner.Neighborhood = item.Neighborhood;
                    internetPartner.Number = item.Number;
                    internetPartner.Phone = item.Phone;
                    internetPartner.State = item.State;
                    internetPartner.Update = DateTime.Now;
                    internetPartner.Zipcode = item.Zipcode;

                    internetPartnerRepository.Add(internetPartner);

                    var internetPartnerLocation = new InternetPartnerLocation() { StoreID = internetPartner.StoreID, StoreKeeperID = internetPartner.StoreKeeperID };

                    var address = internetPartner.Logradouro + ", " + internetPartner.Number + ", " + internetPartner.City + " - " + internetPartner.State + ", República Federativa do Brasil‏";

                    WebRequest request = WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address=" + address);

                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            XDocument document = XDocument.Load(new StreamReader(stream));

                            XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                            XElement latitudeElement = document.Descendants("lat").FirstOrDefault();

                            if (longitudeElement != null && latitudeElement != null)
                            {
                                internetPartnerLocation.Latitude = latitudeElement.Value;
                                internetPartnerLocation.Longitude = longitudeElement.Value;

                                internetPartnerLocationRepository.Add(internetPartnerLocation);
                            }
                        }
                    }
                }
            }

            foreach (var item in list)
            {
                if (!fileList.ToList().Any(x => x.StoreKeeperID.Contains(item.StoreKeeperID) && x.StoreID.Contains(item.StoreID)))
                {
                    internetPartnerRepository.Delete(item);
                    var itemLocation = internetPartnerLocationRepository.Get(x => x.StoreID == item.StoreID && x.StoreKeeperID == x.StoreKeeperID).FirstOrDefault();

                    if (itemLocation != null)
                        internetPartnerLocationRepository.Delete(itemLocation);
                }
            }
        }

        public static string GenerateScriptSync(IInternetPartnerRepository internetPartnerRepository, IInternetPartnerLocationRepository internetPartnerLocationRepository)
        {
            try
            {
                var list = internetPartnerRepository.GetAll();

                var update = "UPDATE CorrespondentesInternet SET Lojista = '{0}', Loja = '{1}', CNPJ = '{2}', Nome = '{3}', Abreviatura = '{4}', Telefone = '{5}', Logradouro = '{6}', Numero = '{7}', Complemento = '{8}', Bairro = '{9}', Cidade = '{10}', UF = '{11}', CEP = '{12}', Segmentacao = '{13}', DataAtualizacao = {14}, LojaMedicalEmergencyGF = {15}, CidadeID = {16}, WhatsApp = '{17}', PartnerCode = '{18}', UserCode = '{19}', Password = '{20}' WHERE Lojista = '{21}' AND Loja = '{22}';";

                var addInternetPartner = "INSERT INTO CorrespondentesInternet (Lojista, Loja, CNPJ, Nome, Abreviatura, Telefone, Logradouro, Numero, Complemento, Bairro, Cidade, UF, CEP, Segmentacao, DataAtualizacao, LojaMedicalEmergencyGF, CidadeID, WhatsApp, PartnerCode, UserCode, Password) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', {14}, {15}, {16}, '{17}', '{18}', '{19}', '{20}');";

                var addInternetPartnerLocation = "INSERT INTO CorrespondentesInternetLocalizacao (Lojista, Loja, latitude, longitude, DataAtualizacao) VALUES('{0}', '{1}', '{2}', '{3}', {4});";

                var deleteInternetPartner = "DELETE FROM CorrespondentesInternet WHERE Lojista = '{0}' AND Loja = {1};";
                var deleteInternetPartnerLocation = "DELETE FROM CorrespondentesInternetLocalizacao WHERE Lojista = '{0}' AND Loja = {1};";

                var file = new ExcelQueryFactory(@"C:\Users\eduardo.fernandes\Downloads\CORRESPONDENTES_25_01_2017.xlsx");

                var fileList = file.Worksheet<InternetPartnerImportViewModel>("Plan1");

                using (FileStream fileStream = File.Create((@"C:\Users\eduardo.fernandes\Downloads\Script.sql")))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        foreach (InternetPartnerImportViewModel item in fileList)
                        {
                            var internetPartner = list.Where(x => x.StoreKeeperID == item.StoreKeeperID && x.StoreID == item.StoreID).FirstOrDefault();

                            if (internetPartner != null)
                            {
                                internetPartner.StoreID = item.StoreID ?? internetPartner.StoreID;
                                internetPartner.StoreKeeperID = item.StoreKeeperID ?? internetPartner.StoreKeeperID;
                                internetPartner.Name = item.Name ?? internetPartner.Name;
                                internetPartner.Logradouro = item.Logradouro ?? internetPartner.Logradouro;
                                internetPartner.Abbreviation = item.OwnStoreName ?? internetPartner.Abbreviation;
                                internetPartner.City = item.City ?? internetPartner.City;
                                internetPartner.CNPJ = item.CNPJ ?? internetPartner.CNPJ;
                                internetPartner.Complement = item.Complement ?? internetPartner.Complement;
                                internetPartner.Neighborhood = item.Neighborhood ?? internetPartner.Neighborhood;
                                internetPartner.Number = item.Number ?? internetPartner.Number;
                                internetPartner.Phone = item.Phone ?? internetPartner.Phone;
                                internetPartner.State = item.State ?? internetPartner.State;
                                internetPartner.Update = DateTime.Now;
                                internetPartner.Zipcode = item.Zipcode ?? internetPartner.Zipcode;
                                internetPartner.WhatsApp = item.WhatsApp ?? internetPartner.WhatsApp;
                                internetPartner.MedicalEmergencyGFStore = item.OwnStoreName != null;

                                if (internetPartner.CityID == null)
                                    streamWriter.WriteLine(update, internetPartner.StoreKeeperID, internetPartner.StoreID, internetPartner.CNPJ, internetPartner.Name, internetPartner.Abbreviation, internetPartner.Phone, internetPartner.Logradouro, internetPartner.Number, internetPartner.Complement, internetPartner.Neighborhood, internetPartner.City, internetPartner.State, internetPartner.Zipcode, "Prata", "GETDATE()", internetPartner.MedicalEmergencyGFStore == true ? 1 : 0, "NULL", internetPartner.WhatsApp, internetPartner.PartnerCode, internetPartner.UserCode, internetPartner.Password, internetPartner.StoreKeeperID, internetPartner.StoreID);
                                else
                                    streamWriter.WriteLine(update, internetPartner.StoreKeeperID, internetPartner.StoreID, internetPartner.CNPJ, internetPartner.Name, internetPartner.Abbreviation, internetPartner.Phone, internetPartner.Logradouro, internetPartner.Number, internetPartner.Complement, internetPartner.Neighborhood, internetPartner.City, internetPartner.State, internetPartner.Zipcode, "Prata", "GETDATE()", internetPartner.MedicalEmergencyGFStore == true ? 1 : 0, internetPartner.CityID, internetPartner.WhatsApp, internetPartner.PartnerCode, internetPartner.UserCode, internetPartner.Password, internetPartner.StoreKeeperID, internetPartner.StoreID);

                                streamWriter.WriteLine("GO");
                            }
                            else
                            {
                                internetPartner = new InternetPartner();

                                internetPartner.StoreID = item.StoreID;
                                internetPartner.StoreKeeperID = item.StoreKeeperID;
                                internetPartner.Name = item.Name;
                                internetPartner.Logradouro = item.Logradouro;
                                internetPartner.Abbreviation = item.OwnStoreName != null ? item.OwnStoreName : item.Name;
                                internetPartner.City = item.City;
                                internetPartner.CNPJ = item.CNPJ;
                                internetPartner.Complement = item.Complement;
                                internetPartner.Neighborhood = item.Neighborhood;
                                internetPartner.Number = item.Number;
                                internetPartner.Phone = item.Phone;
                                internetPartner.State = item.State;
                                internetPartner.Update = DateTime.Now;
                                internetPartner.Zipcode = item.Zipcode;
                                internetPartner.MedicalEmergencyGFStore = item.OwnStoreName != null;

                                streamWriter.WriteLine(addInternetPartner, internetPartner.StoreKeeperID, internetPartner.StoreID, internetPartner.CNPJ, internetPartner.Name, internetPartner.Abbreviation, internetPartner.Phone, internetPartner.Logradouro, internetPartner.Number, internetPartner.Complement, internetPartner.Neighborhood, internetPartner.City, internetPartner.State, internetPartner.Zipcode, "Prata", "GETDATE()", internetPartner.MedicalEmergencyGFStore == true ? 1 : 0, "NULL", internetPartner.WhatsApp, internetPartner.PartnerCode, internetPartner.UserCode, internetPartner.Password);
                                streamWriter.WriteLine("GO");

                                var internetPartnerLocation = new InternetPartnerLocation() { StoreID = internetPartner.StoreID, StoreKeeperID = internetPartner.StoreKeeperID };

                                var address = internetPartner.Logradouro + ", " + internetPartner.Number + ", " + internetPartner.City + " - " + internetPartner.State + ", República Federativa do Brasil‏";

                                WebRequest request = WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address=" + address);

                                using (WebResponse response = request.GetResponse())
                                {
                                    using (Stream stream = response.GetResponseStream())
                                    {
                                        XDocument document = XDocument.Load(new StreamReader(stream));

                                        XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                                        XElement latitudeElement = document.Descendants("lat").FirstOrDefault();

                                        if (longitudeElement != null && latitudeElement != null)
                                        {
                                            internetPartnerLocation.Latitude = latitudeElement.Value;
                                            internetPartnerLocation.Longitude = longitudeElement.Value;

                                            streamWriter.WriteLine(addInternetPartnerLocation, internetPartnerLocation.StoreKeeperID, internetPartnerLocation.StoreID, internetPartnerLocation.Latitude, internetPartnerLocation.Longitude, "GETDATE()");
                                            streamWriter.WriteLine("GO");
                                        }
                                    }
                                }
                            }
                        }

                        foreach (var item in list)
                            if (!fileList.ToList().Any(x => x.StoreKeeperID == item.StoreKeeperID && x.StoreID == item.StoreID))
                            {
                                streamWriter.WriteLine(deleteInternetPartner, item.StoreKeeperID, item.StoreID);
                                streamWriter.WriteLine("GO");

                                var itemLocation = internetPartnerLocationRepository.Get(x => x.StoreID == item.StoreID && x.StoreKeeperID == x.StoreKeeperID).FirstOrDefault();

                                if (itemLocation != null)
                                {
                                    streamWriter.WriteLine(deleteInternetPartnerLocation, item.StoreKeeperID, item.StoreID);
                                    streamWriter.WriteLine("GO");
                                }
                            }

                        streamWriter.WriteLine("UPDATE CorrespondentesInternet SET CidadeID = cid_id FROM pt_cidade INNER JOIN CorrespondentesInternet on CorrespondentesInternet.Cidade = pt_cidade.cid_nome; GO");
                    }
                }

                return null;
            }
            catch (Exception exception) { return exception.Message; }
        }
    }
}