# RealMarketAPI.Sdk

Official .NET SDK for the [RealMarket API](https://realmarketapi.com) — providing real-time market prices, OHLCV candles, historical data, technical indicators, WebSocket streaming, advanced market analysis (Insight, Liquidity, Order Flow, Stop Hunt, Anomaly, Manipulation), and MCP (Model Context Protocol) support.

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
    public async Task BasicExamplesAsync()
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

    public async Task InsightExamplesAsync()
    {
        // Next-candle forecast with bias and ATR targets
        var next = await client.Insight.GetNextAsync("BTCUSD", "H1");
        Console.WriteLine($"Bias: {next.Bias}  Bull: {next.BullScore}  Bear: {next.BearScore}");
        Console.WriteLine($"Target Up: {next.TargetUp}  Target Down: {next.TargetDown}");

        // Trend classification
        var trend = await client.Insight.GetTrendAsync("EURUSD", "H4");
        Console.WriteLine($"Trend: {trend.Trend}  ADX: {trend.Adx}");

        // Market setup detection
        var setup = await client.Insight.GetSetupAsync("XAUUSD", "M15");
        Console.WriteLine($"Setup: {setup.Setup}  Direction: {setup.Direction}");

        // Confluence signal with strength and reasons
        var confluence = await client.Insight.GetConfluenceAsync("BTCUSD", "H1");
        Console.WriteLine($"Signal: {confluence.Signal}  Strength: {confluence.Strength}  Score: {confluence.Score}");

        // Composite 0–100 bullish/bearish score
        var score = await client.Insight.GetScoreAsync("EURUSD", "H1");
        Console.WriteLine($"Score: {score.Score}  Label: {score.Label}");
    }

    public async Task ProModulesExamplesAsync()
    {
        // Trend direction across all timeframes
        var mtf = await client.MultiTimeframe.GetAsync("BTCUSD");
        foreach (var (tf, direction) in mtf.Timeframes)
            Console.WriteLine($"{tf}: {direction}");

        // Liquidity zones (S/R clusters)
        var liquidity = await client.Liquidity.GetZonesAsync("XAUUSD", "H1");
        foreach (var zone in liquidity.Zones)
            Console.WriteLine($"{zone.Type} @ {zone.Price}  Strength: {zone.Strength}  Touches: {zone.TouchCount}");

        // Order flow imbalance
        var imbalance = await client.OrderFlow.GetImbalanceAsync("BTCUSD", "M15");
        Console.WriteLine($"Imbalance: {imbalance.CurrentImbalance}  Bull%: {imbalance.BullishRatio}  Bear%: {imbalance.BearishRatio}");

        // Stop hunt zones
        var stopHunt = await client.StopHunt.GetZonesAsync("EURUSD", "H1");
        foreach (var zone in stopHunt.Zones)
            Console.WriteLine($"{zone.Type} @ {zone.Price}  Hunted: {zone.RecentlyHunted}");

        // Anomaly scan
        var anomalies = await client.Anomaly.GetAsync("BTCUSD", "M15");
        if (anomalies.HasAnomalies)
            foreach (var a in anomalies.Anomalies)
                Console.WriteLine($"{a.Type} at {a.OpenTime}: {a.Description}");

        // Manipulation risk
        var risk = await client.Manipulation.GetRiskAsync("XAUUSD", "M15");
        Console.WriteLine($"Risk: {risk.RiskLevel}  Score: {risk.RiskScore}");
    }

    public async Task StreamPricesAsync(CancellationToken ct)
    {
        // Real-time price ticks (requires WebSocket-enabled plan)
        await foreach (var tick in client.WebSocket.StreamPriceAsync("EURUSD", "M1", ct))
            Console.WriteLine($"[WS price] {tick.OpenTime}: Close={tick.ClosePrice}");

        // Live order flow imbalance updates (PRO+)
        await foreach (var imbalance in client.WebSocket.StreamOrderFlowImbalanceAsync("BTCUSD", "H1", ct))
            Console.WriteLine($"[WS orderflow] {imbalance.CurrentImbalance}  Bull%: {imbalance.BullishRatio}");

        // Live multi-timeframe trend updates (PRO+)
        await foreach (var mtf in client.WebSocket.StreamMultiTimeframeAsync("EURUSD", ct))
            foreach (var (tf, direction) in mtf.Timeframes)
                Console.WriteLine($"[WS mtf] {tf}: {direction}");

        // Full market snapshot on every H1 tick
        await foreach (var market in client.WebSocket.StreamMarketAsync(ct))
            Console.WriteLine($"[WS market] received {market.Count} symbols");
    }
}
```

## Available Endpoints

### Ticker (`client.Ticker`)

| Method | Description |
|--------|-------------|
| `GetPriceAsync(symbol, timeframe)` | Latest real-time ticker with bid/ask |
| `GetMarketPricesAsync()` | Market overview for all plan symbols |
| `GetPriceByCategoryAsync(category)` | Market prices filtered by category (`Forex`, `Crypto`, `Commodity`, `Equity`, `Stock`, `Index`) |
| `Get24hrStatsAsync()` | 24-hour open/close/high/low/volume and change % per symbol |
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

### Account (`client.Account`)

| Method | Description |
|--------|-------------|
| `GetMeAsync()` | Plan code, request count/limit, usage %, allowed symbols & timeframes, WebSocket limits, historical range |

> Does **not** consume your request quota.

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

### Insight (`client.Insight`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetNextAsync(symbol, timeframe)` | Next-candle forecast: bias, bull/bear score, ATR-based targets, and 5 signals |
| `GetTrendAsync(symbol, timeframe)` | Trend classification using EMA alignment and ADX strength |
| `GetSetupAsync(symbol, timeframe)` | Market setup detection: Breakout, Pullback, Range, or None |
| `GetConfluenceAsync(symbol, timeframe)` | Actionable signal (Buy / Sell / Neutral) with strength rating and scored reasons |
| `GetScoreAsync(symbol, timeframe)` | Composite 0–100 bullish/bearish score from five equally-weighted components |

### Multi-Timeframe (`client.MultiTimeframe`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetAsync(symbol)` | Trend direction across all plan-allowed timeframes in a single call |

### Liquidity (`client.Liquidity`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetZonesAsync(symbol, timeframe)` | Key S/R clusters acting as liquidity pools, sorted nearest-first |

### Order Flow (`client.OrderFlow`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetImbalanceAsync(symbol, timeframe)` | Bullish/Bearish/Neutral imbalance from last 50 candles + abnormal-body zones |

### Stop Hunt (`client.StopHunt`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetZonesAsync(symbol, timeframe)` | Stop-cluster zones beyond key S/R levels; flags recently hunted levels |

### Anomaly (`client.Anomaly`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetAsync(symbol, timeframe)` | Scans last 50 candles for PriceSpike, UnusualVolume, and FakeBreakout anomalies |

### Manipulation Risk (`client.Manipulation`) — *Pro plan required*

| Method | Description |
|--------|-------------|
| `GetRiskAsync(symbol, timeframe)` | Composite manipulation risk score (0–100) from wick ratio, volume divergence, and fake breakouts |

### WebSocket (`client.WebSocket`)

| Method | Parameters | Plan | Description |
|--------|-----------|------|-------------|
| `StreamPriceAsync` | `symbol, timeframe, ct` | Socket-enabled | Real-time price ticks as `IAsyncEnumerable<PriceTickerResult>` |
| `StreamCandlesAsync` | `symbol, timeframe, ct` | Socket-enabled | Real-time OHLCV candle updates |
| `StreamOrderFlowImbalanceAsync` | `symbol, timeframe, ct` | PRO+ | Live order flow imbalance updates |
| `StreamMultiTimeframeAsync` | `symbol, ct` | PRO+ | Trend direction across all timeframes, pushed on any candle update |
| `StreamMarketAsync` | `ct` | Socket-enabled | Full market snapshot pushed on every H1 tick |

> Requires a plan with `IsSocketSupport = true`.  
> PRO+ endpoints additionally require the PRO plan or above.  
> Endpoints: `wss://api.realmarketapi.com/{price|candles|orderflow/imbalance|multi-timeframe|market}`

### MCP — Model Context Protocol (`client.Mcp`)

Exposes the full RealMarket API as MCP tools, callable from AI assistants (GitHub Copilot, Claude, etc.) and from .NET code.  
Endpoint: `https://api.realmarketapi.com/mcp`

> **MCP calls do not consume your request quota.**

#### Market Data & Indicators

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

#### Advanced Analysis — *Pro plan required*

| Method | MCP Tool | Description |
|--------|----------|-------------|
| `GetOrderFlowImbalanceAsync(symbol, timeframe)` | `get_orderflow_imbalance` | Bullish/bearish candle dominance + large-body imbalance zones |
| `GetMultiTimeframeAsync(symbol)` | `get_multi_timeframe` | Trend direction across all available timeframes |
| `GetStopHuntZonesAsync(symbol, timeframe)` | `get_stop_hunt_zones` | S/R levels likely targeted by stop runs |
| `GetLiquidityZonesAsync(symbol, timeframe)` | `get_liquidity_zones` | Bid/ask liquidity clusters (swing high/low touch count) |
| `GetAnomalyAsync(symbol, timeframe)` | `get_anomaly` | Price spike, unusual volume, and fake breakout detection |
| `GetManipulationRiskAsync(symbol, timeframe)` | `get_manipulation_risk` | Manipulation risk score (0–100) with contributing factors |
| `GetInsightScoreAsync(symbol, timeframe)` | `get_insight_score` | Composite bullish/bearish score (0–100) + label |
| `GetInsightTrendAsync(symbol, timeframe)` | `get_insight_trend` | Trend direction, strength, and ADX momentum |
| `GetInsightSetupAsync(symbol, timeframe)` | `get_insight_setup` | Market setup detection (Breakout / Pullback / Range) |
| `GetInsightNextAsync(symbol, timeframe)` | `get_insight_next` | Next-candle forecast with bias and ATR-based targets |
| `GetInsightConfluenceAsync(symbol, timeframe)` | `get_insight_confluence` | Per-indicator directional votes + confluence score |

> Indicator and Advanced Analysis MCP tools require a **Pro** plan or higher.  
> Volatility MCP tools require a **Starter** plan or higher.

## Notes

- **Pro plan** required for: Indicator, Insight, Multi-Timeframe, Liquidity, Order Flow, Stop Hunt, Anomaly, and Manipulation endpoints.
- **Starter plan** required for Volatility endpoints (Free plan not supported).
- WebSocket streaming requires a plan with `IsSocketSupport = true`. PRO+ endpoints additionally require the PRO plan.
- **MCP calls do not consume your request quota** — they are treated as tooling/agent access.
- **Market-aware quota**: REST requests for symbols whose market is currently closed do not count against your monthly quota.
- Historical data availability depends on your plan's `HistoricalRangeMonth`.
- `GET /me` (`client.Account.GetMeAsync()`) does not consume your request quota.
- All methods accept an optional `CancellationToken`.
- Targets **.NET 10**.
