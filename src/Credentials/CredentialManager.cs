using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zerolingo.Credentials {
    public class CredentialManager {
        private string filePath;
        public LoginCredentials duolingoCredentials { get; set; }
        public LoginCredentials googleCredentials { get; set; } 
        public CredentialManager(string @path) {
            filePath = path;
        }
        public async Task InitializeCredentialFile() {
            if (!File.Exists(filePath)) {
                CredentialFile file = new CredentialFile {
                    Duolingo_Credentials = new LoginCredentials {
                        Username = string.Empty,
                        Password = string.Empty
                    },
                    Google_Credentials = new LoginCredentials {
                        Username = string.Empty,
                        Password = string.Empty
                    },
                    IsGoogleAccount = false
                };

                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await JsonSerializer.SerializeAsync<CredentialFile>(stream, file, new JsonSerializerOptions { WriteIndented = true });
            } else {
                    string fileContents = await File.ReadAllTextAsync(filePath, System.Text.Encoding.UTF8);
                    CredentialFile file = JsonSerializer.Deserialize<CredentialFile>(fileContents);

                    duolingoCredentials = file.Duolingo_Credentials;
                    googleCredentials = file.Google_Credentials;
            }
        }
    }
}