# RealMarketAPI.Sdk

Official .NET SDK for the [RealMarket API](https://realmarketapi.com) — providing real-time market prices, OHLCV candles, historical data, technical indicators, WebSocket streaming, and MCP (Model Context Protocol) support.

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)
![NuGet](https://img.shields.io/nuget/v/RealMarketAPI.Sdk)
![License](https://img.shields.io/badge/license-MIT-blue)

## Installation

```bash
dotnet add package RealMarketAPI.Sdk
```

## Quick Start

### 1. Register with Dependency Injection

```csharp
// Program.cs or Startup.cs
builder.Services.AddRealMarketApiClient("YOUR_API_KEY");
```

Or with full options:

```csharp
builder.Services.AddRealMarketApiClient(options =>
{
    options.ApiKey = "YOUR_API_KEY";
    options.BaseUrl = "https://api.realmarketapi.com/"; // default
});
```

### 2. Inject and Use

```csharp
public class MarketService(IRealMarketApiClient client)
{
    public async Task PrintPriceAsync()
    {
        // Real-time price
        var price = await client.Ticker.GetPriceAsync("EURUSD", "M1");
        Console.WriteLine($"EURUSD Close: {price.ClosePrice}");

        // Latest OHLCV candles
        var candles = await client.Ticker.GetCandlesAsync("BTCUSDT", "H1");
        foreach (var c in candles.Data)
            Console.WriteLine($"{c.OpenTime}: O={c.OpenPrice} H={c.HighPrice} L={c.LowPrice} C={c.ClosePrice}");

        // SMA(20)
        var sma = await client.Indicators.GetSmaAsync("EURUSD", "H1", period: 20);

        // RSI(14)
        var rsi = await client.Indicators.GetRsiAsync("EURUSD", "H1");

        // MACD
        var macd = await client.Indicators.GetMacdAsync("EURUSD", "H1");

        // Available symbols
        var symbols = await client.Symbols.GetSymbolsAsync();
    }

    public async Task StreamPricesAsync(CancellationToken ct)
    {
        // Real-time WebSocket streaming (requires WebSocket-enabled plan)
        await foreach (var tick in client.WebSocket.StreamPriceAsync("EURUSD", "M1", ct))
            Console.WriteLine($"[WS] {tick.OpenTime}: Close={tick.ClosePrice}");
    }
}
```

## Available Endpoints

### Ticker (`client.Ticker`)

| Method | Description |
|--------|-------------|
| `GetPriceAsync(symbol, timeframe)` | Latest real-time ticker with bid/ask |
| `GetMarketPricesAsync()` | Market overview for all plan symbols |
| `GetCandlesAsync(symbol, timeframe)` | Latest OHLCV candles |
| `GetHistoryAsync(symbol, start, end, page, size)` | Paginated historical candle data |

### Indicators (`client.Indicators`)

| Method | Description |
|--------|-------------|
| `GetSmaAsync(symbol, timeframe, period)` | Simple Moving Average |
| `GetEmaAsync(symbol, timeframe, period)` | Exponential Moving Average |
| `GetRsiAsync(symbol, timeframe, period=14)` | Relative Strength Index |
| `GetMacdAsync(symbol, timeframe, fast=12, slow=26, signal=9)` | MACD line, signal, and histogram |
| `GetBollingerBandsAsync(symbol, timeframe, period=20, multiplier=2)` | Bollinger Bands (upper, middle, lower) |
| `GetStochasticAsync(symbol, timeframe, kPeriod=14, dPeriod=3)` | Stochastic Oscillator (%K and %D) |
| `GetAtrAsync(symbol, timeframe, period=14)` | Average True Range |
| `GetCciAsync(symbol, timeframe, period=20)` | Commodity Channel Index |
| `GetWilliamsRAsync(symbol, timeframe, period=14)` | Williams %R |
| `GetAdxAsync(symbol, timeframe, period=14)` | Average Directional Index (+DI, -DI) |
| `GetSupportResistanceAsync(symbol, timeframe)` | Support and resistance levels |
| `GetFibonacciAsync(symbol, timeframe, lookback=100)` | Fibonacci retracement levels |
| `GetSentimentAsync(symbol, timeframe)` | Market sentiment (trend, fear/greed score) |

### Symbols (`client.Symbols`)

| Method | Description |
|--------|-------------|
| `GetSymbolsAsync()` | All available trading symbols for your plan |

### Volatility (`client.Volatility`)

| Method | Description |
|--------|-------------|
| `GetVolatilityAsync(symbol, timeframe, period=14)` | Time-series of ATR, ATR%, Bollinger Band Width, and Historical Volatility |
| `GetSpikesAsync(symbol, timeframe, period=14, spikeMultiplier=2.0)` | Candles where ATR exceeded a multiple of the series average |
| `GetHeatmapAsync(symbol, timeframe)` | Day-of-Week × Hour-of-Day average true range heatmap |

> Available on Starter plan and above. Free plan is not supported.

### WebSocket (`client.WebSocket`)

| Method | Description |
|--------|-------------|
| `StreamPriceAsync(symbol, timeframe, ct)` | Stream real-time price ticks as `IAsyncEnumerable<PriceTickerResult>` |

> Requires a plan with WebSocket support enabled (`IsSocketSupport = true`).
> Endpoint: `wss://api.realmarketapi.com/price`

### MCP — Model Context Protocol (`client.Mcp`)

Exposes the full RealMarket API as MCP tools, callable from AI assistants (GitHub Copilot, Claude, etc.) and from .NET code.
Endpoint: `https://api.realmarketapi.com/mcp`

| Method | MCP Tool | Description |
|--------|----------|-------------|
| `GetPriceAsync(symbol, timeframe)` | `get_price` | Latest real-time price ticker |
| `GetCandlesAsync(symbol, timeframe)` | `get_candles` | Latest OHLCV candles |
| `GetHistoryAsync(symbol, start, end, page, size)` | `get_history` | Paginated historical candles |
| `GetSymbolsAsync()` | `get_symbols` | All available trading symbols |
| `GetTimeframesAsync()` | `get_timeframes` | Timeframe codes supported by your plan |
| `GetSmaAsync(symbol, timeframe, period=20)` | `get_sma` | Simple Moving Average |
| `GetEmaAsync(symbol, timeframe, period=20)` | `get_ema` | Exponential Moving Average |
| `GetRsiAsync(symbol, timeframe, period=14)` | `get_rsi` | Relative Strength Index |
| `GetMacdAsync(symbol, timeframe, fast=12, slow=26, signal=9)` | `get_macd` | MACD line, signal, and histogram |
| `GetBollingerBandsAsync(symbol, timeframe, period=20, multiplier=2)` | `get_bollinger_bands` | Bollinger Bands (upper, middle, lower) |
| `GetStochasticAsync(symbol, timeframe, kPeriod=14, dPeriod=3)` | `get_stochastic` | Stochastic Oscillator (%K and %D) |
| `GetAtrAsync(symbol, timeframe, period=14)` | `get_atr` | Average True Range |
| `GetCciAsync(symbol, timeframe, period=20)` | `get_cci` | Commodity Channel Index |
| `GetWilliamsRAsync(symbol, timeframe, period=14)` | `get_williams_r` | Williams %R |
| `GetAdxAsync(symbol, timeframe, period=14)` | `get_adx` | Average Directional Index (+DI, -DI) |
| `GetSupportResistanceAsync(symbol, timeframe)` | `get_support_resistance` | Support and resistance levels |
| `GetFibonacciAsync(symbol, timeframe, lookback=100)` | `get_fibonacci` | Fibonacci retracement and extension levels |
| `GetSentimentAsync(symbol, timeframe)` | `get_sentiment` | Market sentiment (trend, fear/greed score) |
| `GetVolatilityAsync(symbol, timeframe, period=14)` | `get_volatility` | Volatility time-series (ATR, ATR%, Band Width, Historical Volatility) |
| `GetVolatilitySpikesAsync(symbol, timeframe, period=14, spikeMultiplier=2.0)` | `get_volatility_spikes` | Volatility spike candles |
| `GetVolatilityHeatmapAsync(symbol, timeframe)` | `get_volatility_heatmap` | Day-of-Week × Hour-of-Day volatility heatmap |

> Indicator MCP tools require a **Pro** plan or higher.  
> Volatility MCP tools require a **Starter** plan or higher.

## Notes

- Indicator endpoints require a **Pro** plan or higher.
- Volatility endpoints require a **Starter** plan or higher (Free plan not supported).
- WebSocket streaming requires a plan with `IsSocketSupport = true`.
- Historical data availability depends on your plan's `HistoricalRangeMonth`.
- All methods accept an optional `CancellationToken`.
- Targets **.NET 10**.
