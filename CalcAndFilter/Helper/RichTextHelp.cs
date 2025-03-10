using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CalcAndFilter.Helper
{
    static class RichTextHelp
    {
        public static void SetRichText(this RichTextBox richTextBox, string text,bool doClear)
        {
            if (doClear)
            {
                richTextBox.Document.Blocks.Clear();
            }
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(text));
            richTextBox.Document.Blocks.Add(paragraph);
        }

        public static void RichTextWriteList(this RichTextBox richTextBox, IEnumerable<decimal> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                richTextBox.SetRichText(list.ElementAt(i).ToString(), i == 0);
            }
        }

        public static void FindFilter(this RichTextBox richTextBox,IEnumerable<string> keywords)
        {
            foreach (var keyword in keywords)
            {
                richTextBox.FindFilter(keyword);
            }
        }

        public static void FindFilter(this RichTextBox richTextBox, string keyword)
        {
            // 遍历文档的每个段落，查找关键字并高亮显示
            TextPointer position = richTextBox.Document.ContentStart;
            while (position != null)
            {
                TextPointer next = position.GetNextContextPosition(LogicalDirection.Forward);
                if (next == null)
                {
                    break;
                }
                TextRange textRange = new(position, next);
                if (textRange.Text.Contains(keyword))
                {
                    // 选取关键字的起始位置和结束位置
                    int start = textRange.Text.IndexOf(keyword);
                    int end = start + keyword.Length;
                    TextPointer startPointer = textRange.Start.GetPositionAtOffset(start);
                    TextPointer endPointer = textRange.Start.GetPositionAtOffset(end);
                    // 选取关键字的文本范围
                    TextRange range = new(startPointer, endPointer);
                    // 设置关键字的文本范围的背景色
                    range.ApplyPropertyValue(TextElement.BackgroundProperty, System.Windows.Media.Brushes.Yellow);
                }
                position = next;
            }
        }

        public static void SearchNext(this RichTextBox richTextBox, string keyword)
        {
            TextPointer position = richTextBox.CaretPosition.GetPositionAtOffset(1);
            while (position != null)
            {
                TextPointer next = position.GetNextContextPosition(LogicalDirection.Forward);
                if (next == null)
                {
                    break;
                }
                TextRange textRange = new(position, next);
                if (textRange.Text.Contains(keyword))
                {
                    // 选取关键字的起始位置和结束位置
                    int start = textRange.Text.IndexOf(keyword);
                    int end = start + keyword.Length;
                    TextPointer startPointer = textRange.Start.GetPositionAtOffset(start);
                    TextPointer endPointer = textRange.Start.GetPositionAtOffset(end);
                    // 选取关键字的文本范围
                    TextRange range = new(startPointer, endPointer);
                    richTextBox.CaretPosition = endPointer;
                    richTextBox.ScrollToVerticalOffset(richTextBox.VerticalOffset + 1);
                    break;
                }
                position = next;
            }
        }

        public static void ExportToWord(this RichTextBox richTextBox, string path)
        {
            try
            {
                TextRange textRange = new(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                using var fileStream = System.IO.File.Create(path);
                textRange.Save(fileStream, DataFormats.Rtf);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
