using System;

namespace VideoPokerCli
{
    public static class AlignText
    {
        public static string AlignAndFit(int text, Alignment alignment, int columns)
        {
            return AlignAndFit(text.ToString(), alignment, columns);
        }

        public static string AlignAndFit(string text, Alignment alignment, int columns)
        {
            var formatted = text.Trim();
            formatted = formatted.Substring(0, Math.Min(columns, formatted.Length));

            return alignment switch
            {
                Alignment.Left => LeftText(formatted, columns),
                Alignment.Center => CenterText(formatted, columns),
                Alignment.Right => RightText(formatted, columns),
                _ => formatted,
            };
        }

        private static string LeftText(string text, int columns)
        {
            return text.PadRight(columns);
        }

        private static string CenterText(string text, int columns)
        {
            var padding = (columns - text.Length) / 2m;
            var leftPadding = (int)Math.Floor(padding + text.Length);
            var formatted = text.PadLeft(leftPadding).PadRight(columns);

            return formatted;
        }

        private static string RightText(string text, int columns)
        {
            return text.PadLeft(columns);
        }
    }
}
