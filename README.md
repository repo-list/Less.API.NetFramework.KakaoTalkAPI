# KakaoTalk API
An automation API which is applicable to the software "KakaoTalk for Windows" created and distributed by Kakao Corp.

- 카카오 회사에서 제작하고 배포하는 "KakaoTalk for Windows" 소프트웨어에 적용 가능한 자동화 API입니다.

@ Author : Eric Kim

@ Nickname : Less

@ Email : syusa5537@gmail.com

@ ProductName : Less.API.NetFramework.KakaoTalkAPI

@ Version : 0.2.1

@ License : The Non-Profit Open Software License v3.0 (NPOSL-3.0) (https://opensource.org/licenses/NPOSL-3.0)

- 이 API에는 NPOSL-3.0 오픈소스 라이선스가 적용되며, 사용자는 절대 영리적 목적으로 이 API를 사용해서는 안 됩니다.

@ Other Legal Responsibilities :

- Developers using this automation API should never try to harm or damage servers of "Kakao Corp." by any kinds of approaches.

- *이 자동화 API를 이용하는 개발자들은 절대 어떠한 방법으로도 카카오 회사의 서버에 피해를 입히려는 시도를 해서는 안 됩니다.*

- Developers using this automation API should never try to take any undesired actions which are opposite to the Kakao Terms of Service (http://www.kakao.com/policy/terms?type=s).

- *이 자동화 API를 이용하는 개발자들은 절대 카카오 서비스 약관 (http://www.kakao.com/policy/terms?type=s) 에 반하는 바람직하지 않은 행동들을 취해서는 안 됩니다.*
<br/><br/>

# Version History
@ 0.2.1 (2018-12-08, Latest)

- FriendsTab.StartChattingWith() / ChattingTab.StartChattingAt() -> Deleted way to find a chat using index parameter (perfect way to achieve this not implemented currently)

- *FriendsTab.StartChattingWith() / ChattingTab.StartChattingAt() -> index를 통해 찾는 기능 제거 (현재 완벽한 로직이 구현되지 않은 상태)*


- FriendsTab -> Added StartChattingWithMyself(string myNickname) method.

- *FriendsTab -> StartChattingWithMyself(string myNickname) 메서드 추가. (나 자신과 대화하는 기능)*


- SampleApplication -> Added TestChattingWithMyself() method.

- *SampleApplication -> TestChattingWithMyself() 메서드 추가*


- SampleApplication -> Added induction to invoke KakaoTalk.InitializeManually() method manually, if TestLogin method has not been called.

- *SampleApplication -> TestLogin 메서드를 호출하지 않았을 경우, KakaoTalk.InitializeManually() 메서드를 수동으로 호출하도록 유도하는 내용 추가.*


- SampleApplication.TestRealtimeMessageCheck() -> Changed some method contents and comments.

- *SampleApplication.TestRealtimeMessageCheck() -> 주석 및 메서드 내용 일부 변경*


- Additionally, modified some text messages in SampleApplication.

- *기타 SampleApplication 내 메시지 일부 수정*


@ 0.2.0 (2018-12-05)

- Added support for backing up and restoring clipboard data (only texts and images are available currently)

- *클립보드 데이터를 백업하고 복구하는 기능 지원 (현재 텍스트와 이미지만 가능합니다)*


- Added support for minimizing chat windows while doing an automated work

- *자동화 작업 시에 채팅창을 최소화하는 기능 지원*


- Added and fixed couple of API and sample application comments.

- *API 및 샘플 애플리케이션에 관한 주석 여러 개 수정 및 추가.*


@ 0.1.0 (2018-12-04)

- Initial publish of KakaoTalk API.

- *카카오톡 API 최초 공개.*
