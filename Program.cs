// Kaomoji Snippet
// A simple text expansion tool for quickly typing kaomoji and other text snippets
// Author: half-tree
// GitHub: https://github.com/halfTree-dev
//
// MIT License
//
// Copyright (c) 2025 half-tree
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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