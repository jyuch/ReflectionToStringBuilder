# ReflectionToStringBuilder
ReflectionでToStringするアレ

## About
オブジェクトの文字列形式を動的に生成する為のライブラリです。

## Required Software
本ライブラリは.NET Framework 4以降が必要です。

開発者は以下の環境で開発及び動作確認を行っています。

- Windows 8.1 Pro
- Visual Studio 2015 update 1 Community Edition

## Usagi
使い方はWikiを参照して下さい。

https://github.com/jyuch/ReflectionToStringBuilder/wiki

## ver.1.2からの変更点
- IEnumerableを展開するオプションを追加

## ver.1.1からの変更点
- 手動で文字列形式へマッピングする機能の追加
- 設定を適用しなかった時にデフォルトで割り当てられるインスタンスをキャッシュするように修正

## ver.1.0からの変更点
- パブリックプロパティだけでなく、パブリックなフィールドも文字列形式に含められるようになりました
- クラス名などをより適切な名前へ変更しました
- **上記の変更によってver.1.0と互換性が無くなりました**

## 免責事項
本ライブラリの使用はすべて自己責任で行ってください。

本ライブラリを用いて生じたいかなる損害に対して開発者は一切責任を持ちません。

## License
本ライブラリはMITライセンスのもとで公開されています。
詳しくは[LICENSE](./LICENSE)をご覧ください。
