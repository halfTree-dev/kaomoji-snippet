# Kaomoji-Snippet

This is a kaomoji (emoticon) quick output tool. After opening the program, enter specific prompt words, press the `Ctrl` key, and the program will immediately replace the input prompt with pre-configured kaomoji.

![Sample Usage](sample-use.gif)

## How to Use
Download the binary file from the Release version and run the program. The program will read kaomoji from `snippets-list.json`. In any text box, type the prompt words defined in `snippets-list.json`, press the Ctrl key, and the prompt words will be replaced with the corresponding kaomoji..・ヾ(。＞＜)シ。

The program's source code is also quite simple. You can build it as a .NET project and use it yourself.

(*≧∀≦*) Ｏ(≧▽≦)Ｏ 〒.〒 (・人・)

## Important Notes
1. Prompt words can only be set using `a-z`, `0-9`, and symbols that can be inputted without switching case (e.g., `-`, `[]`, but not `_`);
2. The program uses a global keyboard hook to achieve its functionality, which may not work under some game anti-cheat systems;
3. Printing kaomoji will overwrite the user's clipboard, please be aware.

## Build Dependencies
The program uses the following Nuget packages:
- Newtonsoft.Json
- SharpHook
- TextCopy