using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace riotpls
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;
        private Button btnCheckChampionMastery;
        private Label lblSummonerPuuid; // Add this line

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };

            // Initialize the button for checking champion mastery
            btnCheckChampionMastery = new Button
            {
                Text = "Check Champion Mastery",
                Location = new System.Drawing.Point(150, 120),
                Size = new System.Drawing.Size(150, 30),
                Enabled = false
            };

            // Add the event handler for the new button
            btnCheckChampionMastery.Click += btnCheckChampionMastery_Click;

            // Add the new button to the form's Controls collection
            Controls.Add(btnCheckChampionMastery);

            // Initialize the label for displaying PUUID
            lblSummonerPuuid = new Label
            {
                Text = "Summoner PUUID: ",
                Location = new System.Drawing.Point(10, 160),
                Size = new System.Drawing.Size(300, 20)
            };

            // Add the new label to the form's Controls collection
            Controls.Add(lblSummonerPuuid);
        }

        private async void btnCheckPuuid_Click(object sender, EventArgs e)
        {
            string apiKey = apiKeyTextBox.Text.Trim();
            string gameName = summonerNameTextBox.Text.Trim();
            string tagLine = tagTextBox.Text.Trim();

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(gameName) || string.IsNullOrEmpty(tagLine))
            {
                MessageBox.Show("Please enter Riot API key, game name, and tag line.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string region = "europe"; // Change this to the appropriate region code

            try
            {
                string summonerId = await GetSummonerId(apiKey, region, gameName, tagLine, httpClient);

                if (!string.IsNullOrEmpty(summonerId))
                {
                    Console.WriteLine($"Summoner ID: {summonerId}");

                    // Update the label with the summoner's PUUID
                    lblSummonerPuuid.Text = $"Summoner PUUID: {summonerId}";

                    List<ChampionMastery> championMasteries = await GetChampionMasteries(apiKey, region, summonerId, httpClient);

                    if (championMasteries != null)
                    {
                        Console.WriteLine("Champion Masteries:");
                        foreach (var mastery in championMasteries)
                        {
                            Console.WriteLine($"Champion {mastery.ChampionId}: Mastery Level {mastery.ChampionLevel}, Points {mastery.ChampionPoints}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve summoner ID. Check your input or API key.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                MessageBox.Show($"Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<string> GetSummonerId(string apiKey, string region, string gameName, string tagLine, HttpClient client)
        {
            try
            {
                string apiUrl = $"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{Uri.EscapeDataString(gameName)}/{Uri.EscapeDataString(tagLine)}";

                Console.WriteLine($"API URL for Summoner ID: {apiUrl}");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                var response = await client.GetAsync(apiUrl);

                Console.WriteLine($"Summoner ID Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Summoner ID Response Reason Phrase: {response.ReasonPhrase}");

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{responseBody}");
                if (response.IsSuccessStatusCode)
                {
                    var summoner = JsonSerializer.Deserialize<SummonerResponse>(responseBody);

                    return summoner?.Id;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    MessageBox.Show($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}\n\n{responseBody}", "Summoner ID API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                MessageBox.Show($"HttpRequestException: {ex.Message}", "Summoner ID API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Exception: {ex.Message}");
                MessageBox.Show($"Unexpected Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async void btnCheckChampionMastery_Click(object sender, EventArgs e)
        {
            string apiKey = apiKeyTextBox.Text.Trim();
            string gameName = summonerNameTextBox.Text.Trim();
            string tagLine = tagTextBox.Text.Trim();

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(gameName) || string.IsNullOrEmpty(tagLine))
            {
                MessageBox.Show("Please enter Riot API key, game name, and tag line.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string region = "europe"; // Change this to the appropriate region code

            string summonerId = await GetSummonerId(apiKey, region, gameName, tagLine, httpClient);

            if (!string.IsNullOrEmpty(summonerId))
            {
                List<ChampionMastery> championMasteries = await GetChampionMasteries(apiKey, region, summonerId, httpClient);

                if (championMasteries != null)
                {
                    Console.WriteLine("Champion Masteries:");
                    foreach (var mastery in championMasteries)
                    {
                        Console.WriteLine($"Champion {mastery.ChampionId}: Mastery Level {mastery.ChampionLevel}, Points {mastery.ChampionPoints}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve summoner ID. Check your input or API key.");
            }
        }

        private async Task<List<ChampionMastery>> GetChampionMasteries(string apiKey, string region, string summonerId, HttpClient client)
        {
            try
            {
                string apiUrl = $"https://eun1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{summonerId}";

                Console.WriteLine($"API URL for Champion Mastery: {apiUrl}");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);

                var response = await client.GetAsync(apiUrl);

                Console.WriteLine($"Champion Mastery Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Champion Mastery Response Reason Phrase: {response.ReasonPhrase}");

                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{responseBody}");
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<List<ChampionMastery>>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    MessageBox.Show($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}\n\n{responseBody}", "Champion Mastery API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                MessageBox.Show($"HttpRequestException: {ex.Message}", "Champion Mastery API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Exception: {ex.Message}");
                MessageBox.Show($"Unexpected Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void apiKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckButtonEnabled();
        }

        private void summonerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckButtonEnabled();
        }

        private void tagTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckButtonEnabled();
        }

        private void CheckButtonEnabled()
        {
            btnCheckPuuid.Enabled = apiKeyTextBox.Text.Length > 0 && summonerNameTextBox.Text.Length > 0 && tagTextBox.Text.Length > 0;
            btnCheckChampionMastery.Enabled = apiKeyTextBox.Text.Length > 0 && summonerNameTextBox.Text.Length > 0 && tagTextBox.Text.Length > 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose of resources or perform cleanup as needed
            httpClient.Dispose();
        }
    }

    class SummonerResponse
    {
        public string Id { get; set; }
    }

    class ChampionMastery
    {
        public int ChampionId { get; set; }
        public int ChampionLevel { get; set; }
        public int ChampionPoints { get; set; }
    }

}
