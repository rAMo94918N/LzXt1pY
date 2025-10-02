// 代码生成时间: 2025-10-02 22:41:53
using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SystemPerformanceMonitor
{
    // 定义一个服务类，用于监控系统性能指标
    public class PerformanceMonitorService : IHostedService, IDisposable
    {
        private readonly ILogger<PerformanceMonitorService> _logger;
        private Timer _timer;
        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _ramCounter;

        public PerformanceMonitorService(ILogger<PerformanceMonitorService> logger)
        {
            _logger = logger;
        }

        // 实现IHostedService的StartAsync方法，启动性能监控
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        // 实现IHostedService的StopAsync方法，停止性能监控
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        // 定时执行的方法，用于获取系统性能指标
        private void DoWork(object state)
        {
            try
            {
                float cpuUsage = _cpuCounter.NextValue();
                float availableRam = _ramCounter.NextValue();

                _logger.LogInformation("CPU Usage: {0}%", cpuUsage);
                _logger.LogInformation("Available RAM: {1} MB", availableRam);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while monitoring system performance");
            }
        }

        // 实现IDisposable接口的Dispose方法，释放资源
        public void Dispose()
        {
            _timer?.Dispose();
            _cpuCounter?.Dispose();
            _ramCounter?.Dispose();
        }
    }
}
