# KakaoTalk API
An automation API which is applicable to the software "KakaoTalk for Windows" created and distributed by Kakao Corp. <br/>
-> 카카오 회사에서 제작하고 배포하는 "KakaoTalk for Windows" 소프트웨어에 적용 가능한 자동화 API입니다. <br/><br/>

@ Author : Eric Kim <br/>

@ Nickname : Less, Syusa <br/>

@ Email : syusa5537@gmail.com <br/>

@ ProductName : Less.API.NetFramework.KakaoTalkAPI <br/>

@ Version : 1.0.0 <br/>

@ License : The Non-Profit Open Software License v3.0 (NPOSL-3.0) (https://opensource.org/licenses/NPOSL-3.0) <br/>
- -> 이 API에는 NPOSL-3.0 오픈소스 라이선스가 적용되며, 사용자는 절대 영리적 목적으로 이 API를 사용해서는 안 됩니다. <br/>

@ Other Legal Responsibilities : <br/>
- Developers using this automation API should never try to harm or damage servers of "Kakao Corp." by any kinds of approaches. <br/>
- -> 이 자동화 API를 이용하는 개발자들은 절대 어떠한 방법으로도 카카오 회사의 서버에 피해를 입히려는 시도를 해서는 안 됩니다. <br/>
- Developers using this automation API should never try to take any undesired actions which are opposite to the Kakao Terms of Service (http://www.kakao.com/policy/terms?type=ts). <br/>
- -> 이 자동화 API를 이용하는 개발자들은 절대 카카오 서비스 약관 (http://www.kakao.com/policy/terms?type=ts) 에 반하는 바람직하지 않은 행동들을 취해서는 안 됩니다. <br/><br/>

# Version History
@ 1.0.0 (2019-08-08, Latest) <br/>
- Amended symptom of ClipboardManager sometimes failing at image processing (가끔씩 이미지 프로세싱에 실패하는 현상 해결) <br/>
- Amended Thread-related problems (Thread 관련 문제 수정) <br/><br/>

@ 0.3.0 (2018-12-11) <br/>

- <KakaoTalk.cs> <br/>
- Changed property name (속성 이름 변경) : KakaoTalk.Message.Username -> UserName <br/>
- Restored method contents (메서드 내용 복구) : KTChatWindow.Minimize(), KTChatWindow.Restore() <br/>
- Added comments (주석 추가) : KTChatWindow.RoomName, KTChatWindow.TaskCheckInterval <br/>
- Added methods (메서드 추가) : KTChatWindow.HasTasks(), KTChatWindow.GetTaskCount() <br/>
- Changed method contents (메서드 내용 변경) : KTChatWindow.RunTasks() -> NullPointerException processing, Message.GetMessageType() -> UserJoin, UserLeave detection logic changed (예외 처리, 메시지 타입 감지 로직 변경) <br/>

- <ClipboardManager.cs> <br/>
- Changed method contents (메서드 내용 변경) : BackupData(), RestoreData(), _SetImage() -> Fixed memory leak problem. (메모리 누수 현상 해결) <br/>
- Added comments (주석 추가) : _SetImage() -> ExternalException, ContextSwitchDeadlock related comments (해당 현상들 발생 가능성에 대한 언급 추가) <br/><br/>

@ 0.2.1 (2018-12-08) <br/>

- FriendsTab.StartChattingWith() / ChattingTab.StartChattingAt() -> Deleted way to find a chat using index parameter (perfect way to achieve this not implemented currently) <br/>
- -> FriendsTab.StartChattingWith() / ChattingTab.StartChattingAt() -> index를 통해 찾는 기능 제거 (현재 완벽한 로직이 구현되지 않은 상태) <br/>

- FriendsTab -> Added StartChattingWithMyself(string myNickname) method. <br/>
- -> FriendsTab -> StartChattingWithMyself(string myNickname) 메서드 추가. (나 자신과 대화하는 기능) <br/>

- SampleApplication -> Added TestChattingWithMyself() method. <br/>
- -> SampleApplication -> TestChattingWithMyself() 메서드 추가 <br/>

- SampleApplication -> Added induction to invoke KakaoTalk.InitializeManually() method manually, if TestLogin method has not been called. <br/>
- -> SampleApplication -> TestLogin 메서드를 호출하지 않았을 경우, KakaoTalk.InitializeManually() 메서드를 수동으로 호출하도록 유도하는 내용 추가. <br/>

- SampleApplication.TestRealtimeMessageCheck() -> Changed some method contents and comments. <br/>
- -> SampleApplication.TestRealtimeMessageCheck() -> 주석 및 메서드 내용 일부 변경 <br/>

- Additionally, modified some text messages in SampleApplication. <br/>
- -> 기타 SampleApplication 내 메시지 일부 수정 <br/><br/>

@ 0.2.0 (2018-12-05) <br/>

- Added support for backing up and restoring clipboard data (only texts and images are available currently) <br/>
- -> 클립보드 데이터를 백업하고 복구하는 기능 지원 (현재 텍스트와 이미지만 가능합니다) <br/>

- Added support for minimizing chat windows while doing an automated work <br/>
- -> 자동화 작업 시에 채팅창을 최소화하는 기능 지원 <br/>

- Added and fixed couple of API and sample application comments. <br/>
- -> API 및 샘플 애플리케이션에 관한 주석 여러 개 수정 및 추가. <br/><br/>

@ 0.1.0 (2018-12-04) <br/>

- Initial publish of KakaoTalk API. <br/>
- -> 카카오톡 API 최초 공개. <br/>
