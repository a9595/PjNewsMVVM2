using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using HtmlAgilityPack;

namespace PjNewsMVVM2.Helpers
{
    internal class ArticleContentDownloader
    {
        private static HtmlDocument _htmlDocument;
        private static readonly string baselink = "http://www.pja.edu.pl";


        public string UrlExample { get; set; } =
            "http://www.pja.edu.pl/aktualnosci/nagroda-ministra-kultury-dla-jana-bajtlika";

        public static async Task<List<Block>> GetRichTextBoxContentByLink(string url)
        {
            HtmlNode fullTextNode = await GetFullTextDivHtmlNode(url);
            List<Block> blocks = GenerateBlocksForHtml(fullTextNode.InnerHtml);

            return blocks;
        }

        private async Task buttonClickFunction()
        {


            //Add the blocks to the RichTextBlock
            //rtb1.Blocks.Clear();
            //foreach (Block b in blocks)
            //    rtb1.Blocks.Add(b);
        }





        public static async Task<HtmlNode> GetFullTextDivHtmlNode(string website)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync(website);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(source);



            List<HtmlNode> articleContentHtmlNodes = _htmlDocument.DocumentNode.Descendants().Where
                (x => (x.Name == "div" && x.Attributes["class"] != null &&
                       x.Attributes["class"].Value.Contains("fullText"))).ToList();


            return articleContentHtmlNodes[0];
        }



        #region html_parsing
        private static List<Block> GenerateBlocksForHtml(string xhtml)
        {
            List<Block> bc = new List<Block>();

            // try
            //{
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(xhtml);

            foreach (HtmlNode img in doc.DocumentNode.Descendants("img"))
            {
                if (!img.Attributes["src"].Value.StartsWith("http") && baselink != null)
                {
                    img.Attributes["src"].Value = baselink + img.Attributes["src"].Value;
                }
            }
            foreach (HtmlNode link in doc.DocumentNode.Descendants("a"))
            {
                if (!link.Attributes["href"].Value.StartsWith("http") && baselink != null)
                {
                    link.Attributes["href"].Value = baselink + link.Attributes["href"].Value;
                }
            }
            Block b = GenerateParagraph(doc.DocumentNode);
            bc.Add(b);
            //}
            //catch (Exception ex)
            //{
            //}

            return bc;
        }

        private static string CleanText(string input)
        {
            string clean = Windows.Data.Html.HtmlUtilities.ConvertToText(input);
            //clean = System.Net.WebUtility.HtmlEncode(clean);
            if (clean == "\0")
                clean = "\n";
            return clean;
        }

        private static Block GenerateBlockForTopNode(HtmlNode node)
        {
            return GenerateParagraph(node);
        }


        private static void AddChildren(Paragraph p, HtmlNode node)
        {
            bool added = false;
            foreach (HtmlNode child in node.ChildNodes)
            {
                Inline i = GenerateBlockForNode(child);
                if (i != null)
                {
                    p.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added)
            {
                p.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        private static void AddChildren(Span s, HtmlNode node)
        {
            bool added = false;

            foreach (HtmlNode child in node.ChildNodes)
            {
                Inline i = GenerateBlockForNode(child);
                if (i != null)
                {
                    s.Inlines.Add(i);
                    added = true;
                }
            }
            if (!added)
            {
                s.Inlines.Add(new Run() { Text = CleanText(node.InnerText) });
            }
        }

        private static Inline GenerateBlockForNode(HtmlNode node)
        {
            switch (node.Name.ToLower())
            {
                case "div":
                    return GenerateSpan(node);
                case "p":
                    return GenerateInnerParagraph(node);
                case "img":
                    return GenerateImage(node);
                case "a":
                    if (node.ChildNodes.Count >= 1 && (node.FirstChild.Name == "img" || node.FirstChild.Name == "IMG"))
                        return GenerateImage(node.FirstChild);
                    else
                        return GenerateHyperLink(node);
                case "li":
                    return GenerateLI(node);
                case "b":
                case "strong":
                    return GenerateBold(node);
                case "i":
                    return GenerateItalic(node);
                case "u":
                    return GenerateUnderline(node);
                case "br":
                    return new LineBreak();
                case "span":
                    return GenerateSpan(node);
                case "iframe":
                    return GenerateIFrame(node);
                case "#text":
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                        return new Run() { Text = CleanText(node.InnerText) };
                    break;
                case "h1":
                    return GenerateH1(node);
                case "h2":
                    return GenerateH2(node);
                case "h3":
                    return GenerateH3(node);
                case "ul":
                    return GenerateUL(node);
                default:
                    break; //return GenerateSpanWNewLine(node);
                           //if (!string.IsNullOrWhiteSpace(node.InnerText))
                           //    return new Run() { Text = CleanText(node.InnerText) };
                           //break;
            }
            return null;
        }

        private static Inline GenerateLI(HtmlNode node)
        {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Black);
            ellipse.Width = 6;
            ellipse.Height = 6;
            ellipse.Margin = new Thickness(-30, 0, 0, 1);
            iui.Child = ellipse;
            s.Inlines.Add(iui);
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateImage(HtmlNode node)
        {
            Span s = new Span();
            try
            {
                InlineUIContainer iui = new InlineUIContainer();
                var sourceUri = System.Net.WebUtility.HtmlDecode(node.Attributes["src"].Value);
                Image img = new Image() { Source = new BitmapImage(new Uri(sourceUri, UriKind.Absolute)) };
                img.Stretch = Stretch.Uniform;
                img.VerticalAlignment = VerticalAlignment.Top;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.ImageOpened += img_ImageOpened;
                img.ImageFailed += img_ImageFailed;
                iui.Child = img;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
            }
            catch (Exception ex)
            {
            }
            return s;
        }

        static void img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            var i = 5;
        }

        static void img_ImageOpened(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            BitmapImage bimg = img.Source as BitmapImage;
            if (bimg.PixelWidth > 800 || bimg.PixelHeight > 600)
            {
                img.Width = 800; img.Height = 600;
                if (bimg.PixelWidth > 800)
                {
                    img.Width = 800;
                    img.Height = (800.0 / (double)bimg.PixelWidth) * bimg.PixelHeight;
                }
                if (img.Height > 600)
                {
                    img.Height = 600;
                    img.Width = (600.0 / (double)img.Height) * img.Width;
                }
            }
            else
            {
                img.Height = bimg.PixelHeight;
                img.Width = bimg.PixelWidth;
            }
        }

        private static Inline GenerateHyperLink(HtmlNode node)
        {
            Span s = new Span();
            InlineUIContainer iui = new InlineUIContainer();
            HyperlinkButton hb = new HyperlinkButton() { NavigateUri = new Uri(node.Attributes["href"].Value, UriKind.Absolute), Content = CleanText(node.InnerText) };

            if (node.ParentNode != null && (node.ParentNode.Name == "li" || node.ParentNode.Name == "LI"))
                hb.Style = (Style)Application.Current.Resources["RTLinkLI"];
            else if ((node.NextSibling == null || string.IsNullOrWhiteSpace(node.NextSibling.InnerText)) && (node.PreviousSibling == null || string.IsNullOrWhiteSpace(node.PreviousSibling.InnerText)))
                hb.Style = (Style)Application.Current.Resources["RTLinkOnly"];
            else
                hb.Style = (Style)Application.Current.Resources["RTLink"];

            iui.Child = hb;
            s.Inlines.Add(iui);
            return s;
        }

        private static Inline GenerateIFrame(HtmlNode node)
        {
            try
            {
                Span s = new Span();
                s.Inlines.Add(new LineBreak());
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = Int32.Parse(node.Attributes["width"].Value), Height = Int32.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                s.Inlines.Add(iui);
                s.Inlines.Add(new LineBreak());
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Block GenerateTopIFrame(HtmlNode node)
        {
            try
            {
                Paragraph p = new Paragraph();
                InlineUIContainer iui = new InlineUIContainer();
                WebView ww = new WebView() { Source = new Uri(node.Attributes["src"].Value, UriKind.Absolute), Width = Int32.Parse(node.Attributes["width"].Value), Height = Int32.Parse(node.Attributes["height"].Value) };
                iui.Child = ww;
                p.Inlines.Add(iui);
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Inline GenerateBold(HtmlNode node)
        {
            Bold b = new Bold();
            AddChildren(b, node);
            return b;
        }

        private static Inline GenerateUnderline(HtmlNode node)
        {
            Underline u = new Underline();
            AddChildren(u, node);
            return u;
        }

        private static Inline GenerateItalic(HtmlNode node)
        {
            Italic i = new Italic();
            AddChildren(i, node);
            return i;
        }

        private static Block GenerateParagraph(HtmlNode node)
        {
            Paragraph p = new Paragraph();
            AddChildren(p, node);
            return p;
        }

        private static Inline GenerateUL(HtmlNode node)
        {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateInnerParagraph(HtmlNode node)
        {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            AddChildren(s, node);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateSpan(HtmlNode node)
        {
            Span s = new Span();
            AddChildren(s, node);
            return s;
        }

        private static Inline GenerateSpanWNewLine(HtmlNode node)
        {
            Span s = new Span();
            AddChildren(s, node);
            if (s.Inlines.Count > 0)
                s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Span GenerateH3(HtmlNode node)
        {
            Span s = new Span();
            s.Inlines.Add(new LineBreak());
            Bold bold = new Bold();
            Run r = new Run() { Text = CleanText(node.InnerText) };
            bold.Inlines.Add(r);
            s.Inlines.Add(bold);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH2(HtmlNode node)
        {
            Span s = new Span() { FontSize = 24 };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }

        private static Inline GenerateH1(HtmlNode node)
        {
            Span s = new Span() { FontSize = 30 };
            s.Inlines.Add(new LineBreak());
            Run r = new Run() { Text = CleanText(node.InnerText) };
            s.Inlines.Add(r);
            s.Inlines.Add(new LineBreak());
            return s;
        }
        #endregion

    }
}
