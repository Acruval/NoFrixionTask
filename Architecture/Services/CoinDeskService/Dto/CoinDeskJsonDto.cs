using System.Text.Json.Serialization;

namespace Architecture.Services.CoinDeskService.Dto
{
    public sealed class TickerTime
    {
        [JsonPropertyName("updatedISO")]
        public DateTime UpdatedISO { get; set; }
    }

    public sealed class EUR
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("rate_float")]
        public float Rate{ get; set; }
    }

    public sealed class Bpi
    {
        public EUR EUR { get; set; }
    }

    public sealed class CoinDeskJsonDto
    {
        [JsonPropertyName("time")]
        public TickerTime Time { get; set; }

        [JsonPropertyName("chartName")]
        public string ChartName { get; set; }

        [JsonPropertyName("bpi")]
        public Bpi Bpi { get; set; }
    }
}