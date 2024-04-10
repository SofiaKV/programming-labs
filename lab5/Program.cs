
public enum IdeogramOrigin
{
    Japanese,
    Chinese,
    Korean
}

public interface ITextInfo
{
    void GetTextInfo();
}

public abstract class Text : ITextInfo
{
    protected int characterCount;
    protected string content;

    protected Text(string content)
    {
        this.content = content;
        this.characterCount = content.Length;
    }

    public virtual void GetTextInfo()
    {
        Console.WriteLine($"Кількість символів: {characterCount}");
    }

    public string DetermineAlphabet()
    {
        foreach (char c in content)
        {
            if (c >= 'а' && c <= 'я')
            {
                return "Кирилиця";
            }
            else if (c >= 'a' && c <= 'z')
            {
                return "Латиниця";
            }
            else if (c >= '\u4e00' && c <= '\u9fff')
            {
                return "Китайський";
            }
            else if (c >= '\u3131' && c <= '\ucb4c')
            {
                return "Корейський";
            }
            else if (c >= '\u3040' && c <= '\u309f')
            {
                return "Японський (Хірагана)";
            }
        }

        return "Інший алфавіт";
    }
}

public class Sentence : Text
{
    public Sentence(string content) : base(content)
    {
    }

    public override void GetTextInfo()
    {
        Console.WriteLine($"Речення: {content}");
        base.GetTextInfo();
    }

    public int CountWords()
    {
        string[] words = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}

public class Word : Text
{
    public Word(string content) : base(content)
    {
    }

    public override void GetTextInfo()
    {
        Console.WriteLine($"Слово: {content}");
        base.GetTextInfo();
    }
}

public class Ideogram : Text
{
    private IdeogramOrigin origin;

    public Ideogram(string content, IdeogramOrigin origin) : base(content)
    {
        this.origin = origin;
    }

    public override void GetTextInfo()
    {
        Console.WriteLine($"Ієрогліф, Походження: {origin}");
        base.GetTextInfo();
    }
}

public class Picture : Text
{
    public Picture(string content) : base(content)
    {
    }

    public override void GetTextInfo()
    {
        Console.WriteLine($"Малюнок: {content}");
        base.GetTextInfo();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Console.WriteLine("Введіть речення:");
        string input = Console.ReadLine();

        var sentence = new Sentence(input);
        sentence.GetTextInfo();
        Console.WriteLine($"Кількість слів у реченні: {sentence.CountWords()}");
        Console.WriteLine($"Алфавіт: {sentence.DetermineAlphabet()}");
    }
}

// 你好 - китайський
// 안녕하세요 - корейський
// こんにちは - японський
