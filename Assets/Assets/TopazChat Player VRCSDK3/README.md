# TopazChat Player 3.0.0
TopazChatで配信される音声や映像を視聴するためのVRChatワールドギミックです。

- VRChat SDK3でのみ動作します。VRChat SDK2では動作しません。
- Windows PC上でのみ動作します。Oculus Quest向けのVRChatクライアントでは動作しません。

個人利用では無償でご利用いただけます。法人利用はご相談ください。

# 2.0.0との違い
- 視聴者がVRChatの設定で Allow Untrusted URLs にチェックを入れる必要があります。
- VRChat SDK2では動作しません。
- 複数ストリームの同時視聴ができません（後ほど対応予定です）

# 必要なもの
- Unity 2018.4.20f1
- VRChat SDK3 - Worlds

VRChat SDK2版で必要だったAVPro Videoは必要ありません。

# 使い方
## 設置方法
1. TopazPlayer_3.0.0.unitypackage をUnityプロジェクトにインポートする
2. Projectウィンドウから `Assets/TopazChat Player VRCSDK3/Prefabs/TopazChat Player.prefab` プレハブをシーンに配置
3. ヒエラルキーウィンドウで `TopazChat Player/VideoPlayer` ゲームオブジェクトを選択し、インスペクタウィンドウで開く
4. Udon Behaviourコンポーネントの「Open Udon Graph」ボタンをクリック
5. Udon GraphウィンドウのVariables一覧で「StreamURL」という変数のdefault valueを変更する  
  デフォルトでは `rtspt://topaz.chat/live/StreamKey` となっています。  
  「Stream Key」の部分を任意の英数字へ自由に変更してください。この文字列がストリームキーになります。

さらに以下のようにストリームキーを表示しておくと、制作者以外がTopazChatで配信するときに便利です。

1. ヒエラルキーウィンドウで `TopazChat Player/UI/Address` ゲームオブジェクトを選択し、インスペクタウィンドウで開く
2. Input Fieldコンポーネントの Text をストリームキーに変更（デフォルトではStream Key）

## VRChat上での使い方
VRChatクライアントのSettingsメニューで、Allow Untrusted URLsにチェックを入れてください。視聴者全員が設定する必要があるので、ワールド参加者へのアナウンスが必要です。

また、同設定でWorld音量を上げてください。TopazChatの音声はWorld音声として再生されます。

VRChat上ではResyncボタンとGlobal Syncボタンが表示されます。
- Resyncボタンを押すと、押した人だけ音声・映像を再読み込みします。
- Global Syncボタンを押すと、インスタンス参加者全員が映像・音声を再読み込みします。

インスタンスに参加すると、自動で再生開始されます。インスタンス参加時にAllow Untrusted URLsにチェックが入っていなかった場合は、Resyncボタンで再読み込みしてください。

音声・映像の配信を開始したら、Global Syncボタンを押してください。全員が視聴開始できるようになります。

VRChatの設定メニューを閉じるなどして一時的に音途切れが発生すると、遅延が蓄積することがあります。遅延が気になった場合は、Resyncボタンで自分だけ音声・動画を再読み込みすると直ることがあります。

## 配信方法
### 音声のみ配信する場合
TopazChat Streamerを使用すると、ストリームキーを入れてワンクリックで配信できます。
ダウンロード方法や使い方は以下のURLで確認してください。
https://tyounanmoti.booth.pm/items/1756789

### 映像も配信する場合
映像の配信は映像2Mbps、音声320kbpsの上限ビットレートで試験運用しています。予告なく停止したり、不安定な視聴になったりする可能性があります。

1. OBS等の動画配信ソフトを使用して、下記の設定で配信開始してください。
- サーバー: rtmp://topaz.chat/live
- ストリームキー: プレイヤーの中央に表示されている文字列
- ビットレート: 映像2000kbps以下、音声320kbps以下

2. TopazPlayerの「Global Sync」ボタンをクリック
3. VRChatのSettingsでワールド音声の音量を上げる
4. メニューを閉じたりすると遅延が蓄積するので、たまに「Global Sync」を押すか、各自で「Resync」ボタンを押してもらう

OBSであれば、下記の設定をすると遅延時間が最短（0.5秒ほど）になります。
- 映像
- フレームレート: 60fps
- 出力
- エンコーダ: NVENC
- プリセット: Max Performance
- Profile: High
- Look-ahead: OFF
- 心理視覚チューニング: OFF
- 最大 B フレーム: 0

x264のzerolatencyチューンや、NVENCのLow Latencyプリセットを使用するとVRChat内で映像が描画されないようです。

# 既知の不具合
- 高ビットレートの映像では、再読み込みのたびに再生が安定したり不安定になったりすることがあります
- 複数のストリームを同時視聴できません

# 今後の更新予定
- Stream URLをVRChatクライアント上で変更できるようにする
- 複数のストリームを同時に視聴できるようにする
- 立体音響処理をしないステレオ再生時の距離減衰サンプル

# よくある質問
- Q: 今までのTopazChat Streamerは使えますか？  
  A: はい、これまでと変わらず配信できます。
- Q: 立体音響ではなくステレオで聴かせることはできますか？
  A: はい、Audio Sourceを一つにして、VRCAV Pro Video SpeakerコンポーネントのModeをStereo Mix、VRC Spatial Audio SourceのEnable Spatializationを切れば直接ステレオで聴かせることができます。ただし、参加した瞬間に爆音で再生される可能性があるので、Pan Levelの減衰カーブを書いたり入室時のみ再生開始するようなんらかのギミックを作成してください。今後サンプルを作る予定です。

# 旧バージョン TopazChat Player 2.0.0 について
TopazChat Player 2.x系列は、2021年1月12日のAVPro Video メジャーバージョンアップ(1.x -> 2.0)によりVRChat側が対応するまで動作しなくなりました。既存のワールドの動作には影響ありません。また、既存のAVPro Video 1.x系列を購入済みの方は、引き続きTopazChat Player 2.x系列を利用できます。
TopazChat Player 2.x系列が必要な方は下記URLから過去のバージョンをダウンロードできます。
https://drive.google.com/drive/folders/1ffXUaiejE7xoE_IqGeIILFaDZYotWBuU?usp=sharing

# 謝辞
サーバーに使用しているソフトウェアのライセンス費用は VoxelKei (@VoxelKei) さんが肩代わりしてくださっています。ありがとうございます。

# サーバー料金について
TopazChatの音声・映像転送には費用がかかっており、作者の よしたか がインスタンス維持やデータ転送にかかる費用を支払っています。
下記URLのPixivFANBOXにて月額のカンパを募集していますので、ご協力いただけると助かります。
https://tyounanmoti.fanbox.cc/

# 連絡先
- Twitter: よしたか (@tyounanmoti)
- mailto: tyounan.moti@gmail.com
- Discordサーバー: https://discord.com/invite/fCMcJ8A

# 変更履歴
## 3.0.0
- VRChat SDK3向けにターゲット変更

## 2.0.0
- 最初のリリース
- 2021/01/22 README同梱
- 2020/07/08 README更新
2Mbpsまで配信可能にしました。既存Playerの置き換えは不要です。
- 2020/07/20 ヨーロッパ版パッケージになっていた不具合を修正
rtspt://eu.topaz.chat/ を参照していたのを、rtspt://topaz.chat/ へ正しく修正。2020/07/08よりエンバグしていた。
