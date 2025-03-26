using Newtonsoft.Json;
using SharpHook.Native;
using TextCopy;

public class SnippetText {
    public string text = string.Empty;
    public int power = 1;
}

public class Snippet {
    public string prompt = string.Empty;
    public SnippetText[] snippets = [];
    public int currentIndex = 0;

    private int[] powerPrefix = [];

    public Snippet() { }

    public void UpdateIndex(char? keyChar) {
        if (keyChar == null) { currentIndex = 0; return; }
        if (keyChar == '\b') {
            currentIndex = Math.Max(0, currentIndex - 1);
        }
        else if (currentIndex < prompt.Length && prompt[currentIndex] == keyChar) {
            currentIndex++;
        }
        else {
            currentIndex = 0;
        }
    }

    public void TryPaste() {
        if (powerPrefix.Length == 0) {
            powerPrefix = new int[snippets.Length];
            for (int i = 0; i < snippets.Length; i++) {
                powerPrefix[i] = snippets[i].power + (i == 0 ? 0 : powerPrefix[i - 1]);
            }
        }
        if (currentIndex == prompt.Length) {
            // 删除触发字符串
            for (int i = 0; i < prompt.Length; i++) {
                Thread.Sleep(2);
                SimulateKeyPress(KeyCode.VcBackspace);
                Thread.Sleep(2);
                SimulateKeyRelease(KeyCode.VcBackspace);
            }
            // 使用剪贴板方式输出特殊字符
            string currText = snippets[0].text;
            int rand = new Random().Next(powerPrefix[powerPrefix.Length - 1]);
            for (int i = 0; i < powerPrefix.Length; i++) {
                if (rand < powerPrefix[i]) {
                    currText = snippets[i].text;
                    break;
                }
            }
            // 判断是否是退出片段
            if (currText == "ksexit") {
                Environment.Exit(0);
            }
            // 随机选择一个片段，输出
            ClipboardService.SetText(currText);
            SimulateKeyPress(KeyCode.VcLeftControl);
            Thread.Sleep(2);
            SimulateKeyPress(KeyCode.VcV);
            Thread.Sleep(2);
            SimulateKeyRelease(KeyCode.VcV);
            Thread.Sleep(2);
            SimulateKeyRelease(KeyCode.VcLeftControl);
            currentIndex = 0;
            // 保留原剪贴板内容
        }
    }

    private static void SimulateKeyPress(KeyCode keyCode) {
        Program.eventSimulator.SimulateKeyPress(keyCode);
    }

    private static void SimulateKeyRelease(KeyCode keyCode) {
        Program.eventSimulator.SimulateKeyRelease(keyCode);
    }
}

public class SnippetsList {
    public Snippet[] snippets = [];

    public static SnippetsList FromJson(string json) => JsonConvert.DeserializeObject<SnippetsList>(json) ?? new SnippetsList();
}