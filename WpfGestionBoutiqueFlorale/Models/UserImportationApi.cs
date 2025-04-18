using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfGestionBoutiqueFlorale.Models
{
    public class UserImportationApi
    {
        public static readonly HttpClient client = new HttpClient();

        public static async void BtnImport()
        {
            try
            {
                var users = await GetUsersFromApi();
                EnregistrerUtilisateurs(users);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        public static async Task<List<Utilisateur>> GetUsersFromApi()
        {
            string url = "https://dummyjson.com/users";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(json);
            var users = jsonObject["users"].ToObject<List<Utilisateur>>();

            return users;
        }

        public static void EnregistrerUtilisateurs(List<Utilisateur> users)
        {
            using var db = new GestionFloraleDbContext();

            if (!db.Utilisateurs.Any())
            {
                var utilisateurs = users.Select(u => new Utilisateur
                {
                    //IdUtilisateur = u.id,
                    username = u.username,
                    firstname = u.firstname,
                    lastname = u.lastname,
                    email = u.email,
                    phone = u.phone,
                    role = u.role,     
                    password = u.password,    
                }).ToList();

                db.Utilisateurs.AddRange(utilisateurs);
                db.SaveChanges();
                MessageBox.Show("Utilisateurs importés avec succès !");
            }
        }
    }
}
