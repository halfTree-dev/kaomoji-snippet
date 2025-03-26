using SharpHook;
using SharpHook.Native;

class Program {
    public static EventSimulator eventSimulator = new EventSimulator();

    private static SnippetsList snippetsList = SnippetsList.FromJson(File.ReadAllText("snippets-list.json"));

    private static Dictionary<KeyCode, char> keyMap = new Dictionary<KeyCode, char> {
        { KeyCode.VcSpace, ' ' },
        { KeyCode.VcComma, ',' },
        { KeyCode.VcPeriod, '.' },
        { KeyCode.VcSemicolon, ';' },
        { KeyCode.VcEquals, '=' },
        { KeyCode.VcMinus, '-' },
        { KeyCode.VcSlash, '/' },
        { KeyCode.VcBackslash, '\\' },
        { KeyCode.VcQuote, '\'' },
        { KeyCode.VcOpenBracket, '[' },
        { KeyCode.VcCloseBracket, ']' },
        { KeyCode.VcBackspace, '\b' },
        { KeyCode.VcDelete, '\b' },
        { KeyCode.VcEnter, '\n' }
    };

    static void Main(string[] args) {
        var hook = new SimpleGlobalHook();
        hook.KeyPressed += OnKeyPressed;
        Console.WriteLine("Kaomoji Snippet is running...");
        Console.WriteLine("Type in prompt and press Ctrl to paste snippets.");
        Console.WriteLine($"{ snippetsList.snippets.Length } prompt(s) loaded: { snippetsList.snippets.Select(s => s.prompt).Aggregate((a, b) => $"{a}, {b}") } ");
        hook.Run();
    }

    private static void OnKeyPressed(object? sender, KeyboardHookEventArgs e)
    {
        // 获取按键字符
        var keyChar = GetCharFromKey(e.Data.KeyCode);
        if (e.Data.KeyCode == KeyCode.VcLeftControl || e.Data.KeyCode == KeyCode.VcRightControl) {
            foreach (var snippet in snippetsList.snippets) {
                snippet.TryPaste();
            }
        }
        else {
            foreach (var snippet in snippetsList.snippets) {
                snippet.UpdateIndex(keyChar);
            }
        }
    }

    private static char? GetCharFromKey(KeyCode keyCode) {
        // 映射字母和数字
        if (keyCode >= KeyCode.VcA && keyCode <= KeyCode.VcZ) {
            return (char)('a' + (keyCode - KeyCode.VcA));
        }
        if (keyCode >= KeyCode.Vc0 && keyCode <= KeyCode.Vc9) {
            return (char)('0' + (keyCode - KeyCode.Vc0));
        }
        if (keyCode >= KeyCode.VcNumPad0 && keyCode <= KeyCode.VcNumPad9) {
            return (char)('0' + (keyCode - KeyCode.VcNumPad0));
        }
        if (keyMap.TryGetValue(keyCode, out var mappedChar)) {
            return mappedChar;
        }
        return null;
    }
}

// (≧▽≦) ୧☉□☉୨ (′ʘ⌄ʘ‵) (^～^;)ゞ | ￣︶￣|o
// Hello kaomoji! o(*^▽^*)o