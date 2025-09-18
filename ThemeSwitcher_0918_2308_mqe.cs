// 代码生成时间: 2025-09-18 23:08:56
using System;

namespace ThemeSwitcherApplication
{
    // 枚举，定义不同的主题
    public enum Theme
    {
        Light,
        Dark,
        Custom
    }

    // 异常类，用于处理主题切换时的错误
    public class ThemeSwitchException : Exception
    {
        public ThemeSwitchException(string message) : base(message)
        {
        }
    }

    // 主题切换器类
    public class ThemeSwitcher
    {
        private Theme currentTheme;

        // 构造函数，初始化默认主题为Light
        public ThemeSwitcher()
        {
            currentTheme = Theme.Light;
        }

        // 获取当前主题
        public Theme GetCurrentTheme()
        {
            return currentTheme;
        }

        // 设置新的主题
        public void SetTheme(Theme newTheme)
        {
            if (newTheme != Theme.Custom)
            {
                currentTheme = newTheme;
            }
            else
            {
                throw new ThemeSwitchException("Custom theme is not allowed.");
            }
        }
    }

    // 程序的主入口
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 创建主题切换器实例
                ThemeSwitcher themeSwitcher = new ThemeSwitcher();

                // 切换到Dark主题
                themeSwitcher.SetTheme(Theme.Dark);

                // 打印当前主题
                Console.WriteLine($"The current theme is: {themeSwitcher.GetCurrentTheme()}");
            }
            catch (ThemeSwitchException ex)
            {
                // 错误处理
                Console.WriteLine($"An error occurred while switching themes: {ex.Message}");
            }
        }
    }
}