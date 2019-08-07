using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using Less.API.NetFramework.KakaoTalkAPI;

namespace SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // 원하는 기능의 주석 처리를 해제한 후, 각 메서드에서 요구되는 입력 값들을 수정한 다음 테스트하면 됩니다.
            Console.WriteLine("카카오톡 API Version : " + KakaoTalk.ApiVersion);
            Console.WriteLine();
            Console.WriteLine("사용 방법 : Main 메서드에서 원하는 기능의 주석 처리를 해제한 후, 주석 처리를 해제한 메서드에서 요구되는 입력 값들을 수정한 다음 테스트하면 됩니다.");
            Console.WriteLine();

            //TestLogin(); // 로그인 기능 테스트

            // 아래의 기능들을 테스트할 때는 TestLogin 메서드를 무조건 한 번 먼저 호출해야 합니다.
            // 그렇지 않다면, 카카오톡 로그인이 완료된 상태에서 KakaoTalk.InitializeManually() 메서드를 따로 호출해주어야 합니다.
            // 만약 InitializeManually 메서드 호출 시점에 로그인이 되어 있지 않다면, System.NullReferenceException 예외가 발생합니다.

            //TestFriendsTabFeatures(); // 친구 탭 기능 테스트
            //TestChattingTabFeatures(); // 채팅 탭 기능 테스트
            //TestMultiThreading(); // 멀티스레딩 테스트
            //TestWindowReopen(); // 채팅방 다시 열기 기능 테스트
            //TestRealtimeMessageCheck(); // 실시간 메시지 확인 테스트
            //TestChattingWithMyself(); // 나 자신과 대화 테스트
            //FinalTest(); // 모든 기능 종합 테스트 (로그인 포함)
        }

        static void TestLogin()
        {
            // 수정해야 할 입력값 목록 : tempEmail, tempPassword
            Console.WriteLine("TestLogin 시작");

            try
            {
                KakaoTalk.Run(); // 카카오톡 프로세스 실행
                Console.WriteLine("카카오톡 실행 완료");

                string tempEmail = "카카오 계정(이메일 주소)을 입력합니다";
                string tempPassword = "비밀번호를 입력합니다";
                KakaoTalk.Login(tempEmail, tempPassword); // 로그인
                Console.WriteLine("카카오 로그인 완료");
            }
            catch (KakaoTalk.AlreadyLoggedInException) { KakaoTalk.InitializeManually(); } // 예외 발생 시 수동으로 초기화

            Console.WriteLine("TestLogin 완료");
        }

        static void TestFriendsTabFeatures()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWith
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("TestFriendsTabFeatures 시작");
            
            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);

            using (var chatWindow = friendsTab.StartChattingWith("친구 닉네임")) // 원하는 친구 닉네임 입력
            {
                chatWindow.SendText("카카오톡 API를 통해 보내는 개인 메시지입니다.");
                Thread.Sleep(1000);

                chatWindow.SendImageUsingClipboard(@"res\images\샘플이미지.png");
                Thread.Sleep(1000);

                string nickname = "즐겨찾기 - 3번째 이모티콘"; // 심심풀이로 부여할 닉네임
                int category = KakaoTalk.Emoticon.FavoritesCategory; // 즐겨찾기 카테고리
                int position = 3; // 3번째 이모티콘 (즐겨찾기에 등록된 3번째 이모티콘이 있어야 정상적으로 작동)
                var emoticon1 = new KakaoTalk.Emoticon(nickname, category, position);

                nickname = "기본 이모티콘 - 최고"; // 심심풀이로 부여할 닉네임
                category = KakaoTalk.Emoticon.BasicsCategory; // 기본 이모티콘 카테고리
                position = KakaoTalk.Emoticon.BasicsPosition.최고; // 최고 이모티콘
                var emoticon2 = new KakaoTalk.Emoticon(nickname, category, position);

                nickname = "4번째 카테고리 - 14번째 이모티콘"; // 심심풀이로 부여할 닉네임
                category = 4; // 4번째 카테고리 (즐겨찾기, 기본 이모티콘을 제외한 나머지는 직접 번호로 지정)
                position = 14; // 14번째 이모티콘 (4번째 카테고리의 14번째 이모티콘이 있어야 정상적으로 작동)
                var emoticon3 = new KakaoTalk.Emoticon(nickname, category, position);

                chatWindow.SendEmoticon(emoticon1); // 즐겨찾기 이모티콘 전송
                Thread.Sleep(1000);
                chatWindow.SendEmoticon(emoticon2); // 기본 이모티콘 전송
                Thread.Sleep(1000);
                chatWindow.SendEmoticon(emoticon3); // 4번째 카테고리 이모티콘 전송

                chatWindow.Close(); // 명시적으로 채팅창을 닫을 때 호출. 채팅창을 프로그램 내부적으로 없애기 위해서는
                                    // using 블럭을 사용하거나, Dispose 메서드를 직접 호출해야 함.
            }

            Console.WriteLine("TestFriendsTabFeatures 완료");
        }

        static void TestChattingTabFeatures()
        {
            // 수정해야 할 입력값 목록 : chattingTab.StartChattingAt
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("TestChattingTabFeatures 시작");

            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var chattingTab = KakaoTalk.MainWindow.Chatting;
            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Chatting);

            using (var chatWindow = chattingTab.StartChattingAt("채팅방 이름")) // 원하는 채팅방 이름 입력 (채팅 탭에 해당 이름의 방이 존재해야 합니다)
            {
                chatWindow.SendText("카카오톡 API를 테스트 중입니다...");
                Thread.Sleep(1000);

                chatWindow.SendImageUsingClipboard(@"res\images\샘플이미지.png");
                Thread.Sleep(1000);

                string nickname = "즐겨찾기 - 1번째 이모티콘";
                int category = KakaoTalk.Emoticon.FavoritesCategory;
                int position = 1;
                var emoticon1 = new KakaoTalk.Emoticon(nickname, category, position);

                nickname = "기본 이모티콘 - 야호";
                category = KakaoTalk.Emoticon.BasicsCategory;
                position = KakaoTalk.Emoticon.BasicsPosition.야호;
                var emoticon2 = new KakaoTalk.Emoticon(nickname, category, position);

                nickname = "4번째 카테고리 - 12번째 이모티콘";
                category = 4;
                position = 12;
                var emoticon3 = new KakaoTalk.Emoticon(nickname, category, position);

                chatWindow.SendEmoticon(emoticon1);
                Thread.Sleep(1000);
                chatWindow.SendEmoticon(emoticon2);
                Thread.Sleep(1000);
                chatWindow.SendEmoticon(emoticon3);
                Thread.Sleep(1000);

                chatWindow.SendText("카카오톡 API 테스트를 종료합니다");
                Thread.Sleep(1000);

                chatWindow.Close();
            }

            Console.WriteLine("TestChattingTabFeatures 완료");
        }

        static void TestMultiThreading()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWith, chattingTab.StartChattingAt
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("TestMultiThreading 시작");

            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            var chattingTab = KakaoTalk.MainWindow.Chatting;
            var chatWindows = new List<KakaoTalk.KTChatWindow>();

            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);
            chatWindows.Add(friendsTab.StartChattingWith("친구 닉네임 1"));
            chatWindows.Add(friendsTab.StartChattingWith("친구 닉네임 2"));

            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Chatting);
            chatWindows.Add(chattingTab.StartChattingAt("채팅방 이름 1"));

            var emoticon1 = new KakaoTalk.Emoticon("이모티콘1", KakaoTalk.Emoticon.FavoritesCategory, 1);
            var emoticon2 = new KakaoTalk.Emoticon("이모티콘2", KakaoTalk.Emoticon.BasicsCategory, KakaoTalk.Emoticon.BasicsPosition.야호);
            var emoticon3 = new KakaoTalk.Emoticon("이모티콘3", 4, 12);

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText($"카카오톡 API의 멀티스레딩 기능을 테스트합니다. (Number : {i + 1}/{chatWindows.Count}, RoomName : {chatWindows[i].RoomName})");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("총 5개의 텍스트와 2개의 이미지, 3개의 이모티콘을 교차로 전송합니다.");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 1 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendImageUsingClipboard(@"res\images\샘플이미지.png");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 2 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon1);
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 3 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendImageUsingClipboard(@"res\images\샘플이미지.png");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 4 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon2);
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 5 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon3);
            Thread.Sleep(1000);

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText($"카카오톡 API의 멀티스레딩 기능 테스트를 종료합니다. (Number : {i + 1}/{chatWindows.Count}, RoomName : {chatWindows[i].RoomName})");
            Thread.Sleep(5000);

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].Dispose();

            Console.WriteLine("TestMultiThreading 완료");
        }

        static void TestWindowReopen()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWith
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("TestWindowReopen 시작");

            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);

            using (var chatWindow = friendsTab.StartChattingWith("친구 닉네임"))
            {
                chatWindow.SendText("3초 후에 창을 껐다 켭니다.");
                Thread.Sleep(1000);
                chatWindow.SendText("2초 후에 창을 껐다 켭니다.");
                Thread.Sleep(1000);
                chatWindow.SendText("1초 후에 창을 껐다 켭니다.");
                Thread.Sleep(1000);
                chatWindow.Reopen();
                chatWindow.SendText("창이 다시 열렸습니다.");
                Thread.Sleep(1000);
                chatWindow.SendImageUsingClipboard(@"res\images\샘플이미지.png");
                Thread.Sleep(1000);
            }

            Console.WriteLine("TestWindowReopen 완료");
        }

        static void TestRealtimeMessageCheck()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWith
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            // 이 메서드는 메시지 내용 확인을 통한 디버깅 과정의 예시입니다.
            // 이 메서드가 실행되는 도중 채팅창에 "키워드1"이라는 메시지를 입력할 경우 "키워드 1을 입력하셨습니다."라는 텍스트가 자동으로 전송되며,
            // "키워드2"를 입력할 경우 샘플 이미지가 자동으로 전송됩니다. 또한 "키워드3"을 입력할 경우 이모티콘이 자동으로 전송되며,
            // "종료"를 입력할 경우 채팅을 종료하는 예시입니다.

            // 실제 애플리케이션을 개발할 때는 순간적으로 여러 명이 동시에 메시지를 보낼 수 있기 때문에 lastMessage를 얻어오는 확인 작업이 아닌
            // 현재까지의 검사 인덱스를 저장해 놓고, 그 다음 줄부터 체크하는 방식으로 개발을 진행하는 것이 바람직합니다.
            // 또한, 배열의 길이가 커지면 시스템의 부담이 가중되기 때문에, 중간중간 배열 길이를 검사하여
            // 채팅창을 재실행(KTChatWindow.Reopen())하여 메시지 배열 길이를 줄이는 것이 좋습니다.
            // (채팅창을 껐다가 다시 켰을 때는 저장된 총 대화 개수가 12개 이상이라면, 메시지 배열 길이가 무조건 12로 고정됩니다.)

            Console.WriteLine("TestRealtimeMessageCheck 시작");

            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);

            using (var chatWindow = friendsTab.StartChattingWith("친구 닉네임"))
            {
                KakaoTalk.Message[] messages;
                KakaoTalk.Message lastMessage;
                int loopLength = 1000;
                string keyword1 = "키워드1";
                string keyword2 = "키워드2";
                string keyword3 = "키워드3";
                string quit = "종료";
                for (int i = 0; i < loopLength; i++)
                {
                    messages = chatWindow.GetMessagesUsingClipboard();
                    if (messages == null) continue;
                    lastMessage = messages[messages.Length - 1];
                    Console.WriteLine($"메시지 확인 {i+1}/{loopLength} => 배열 길이 : {messages.Length}, lastMessage : \"{lastMessage.ToString()}\"");
                    if (lastMessage.Content.Equals(keyword1)) chatWindow.SendText("키워드 1을 입력하셨습니다.");
                    else if (lastMessage.Content.Equals(keyword2))
                    {
                        chatWindow.SendText("키워드 2를 입력하셨습니다.");
                        Thread.Sleep(500);
                        chatWindow.SendImageUsingClipboard(@"res\images\샘플이미지.png");
                    }
                    else if (lastMessage.Content.Equals(keyword3))
                    {
                        chatWindow.SendText("키워드 3을 입력하셨습니다.");
                        Thread.Sleep(500);
                        chatWindow.SendEmoticon(new KakaoTalk.Emoticon("이모티콘", KakaoTalk.Emoticon.FavoritesCategory, 1));
                    }
                    else if (lastMessage.Content.Equals(quit))
                    {
                        chatWindow.SendText("채팅을 종료합니다.");
                        Thread.Sleep(500);
                        break;
                    }
                    Thread.Sleep(50);
                }
            }

            Console.WriteLine("TestRealtimeMessageCheck 완료");
        }

        static void TestChattingWithMyself()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWithMyself
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("TestChattingWithMyself 시작");

            if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);

            using (var chatWindow = friendsTab.StartChattingWithMyself("내 닉네임"))
            {
                chatWindow.SendText("카카오톡 API를 통해 보내는 메시지입니다.");
                Thread.Sleep(1000);
                chatWindow.SendText("정상 작동됨을 확인하였습니다.");
            }

            Console.WriteLine("TestChattingWithMyself 완료");
        }

        static void FinalTest()
        {
            // 수정해야 할 입력값 목록 : friendsTab.StartChattingWith, chattingTab.StartChattingAt
            // 이 메서드를 사용하려면 먼저 TestLogin 메서드나, 아래의 KakaoTalk.InitializeManually() 부분을 주석 해제해야 합니다.
            // 그렇지 않을 경우 실행 도중 예외가 발생합니다. (자세한 사항은 Main 메서드 내에 있는 주석을 참고)

            Console.WriteLine("FinalTest 시작");

            //if (!KakaoTalk.IsInitialized()) KakaoTalk.InitializeManually();

            var mainWindow = KakaoTalk.MainWindow;
            var friendsTab = KakaoTalk.MainWindow.Friends;
            var chattingTab = KakaoTalk.MainWindow.Chatting;
            var chatWindows = new List<KakaoTalk.KTChatWindow>();

            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Friends);
            chatWindows.Add(friendsTab.StartChattingWith("친구 닉네임 1"));
            chatWindows.Add(friendsTab.StartChattingWith("친구 닉네임 2"));

            mainWindow.ChangeTabTo(KakaoTalk.MainWindowTab.Chatting);
            chatWindows.Add(chattingTab.StartChattingAt("채팅방 이름 1"));
            chatWindows.Add(chattingTab.StartChattingAt("채팅방 이름 2"));

            var emoticon1 = new KakaoTalk.Emoticon("이모티콘1", KakaoTalk.Emoticon.FavoritesCategory, 1);
            var emoticon2 = new KakaoTalk.Emoticon("이모티콘2", KakaoTalk.Emoticon.BasicsCategory, KakaoTalk.Emoticon.BasicsPosition.야호);
            var emoticon3 = new KakaoTalk.Emoticon("이모티콘3", 4, 12);

            KakaoTalk.Message[] message;
            var buffer = new StringBuilder();

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText($"카카오봇 알파 버전의 모든 기능을 테스트합니다. (Number : {i + 1}/{chatWindows.Count}, RoomName : {chatWindows[i].RoomName})");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText($"총 {chatWindows.Count}개의 방에, 5개의 텍스트와 2개의 이미지 및 3개의 이모티콘을 교차로 전송하며, 전송 도중 현재 메시지 리스트를 가져와서 분석합니다");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 1 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendImageUsingClipboard(@"res\images\오버액션토끼.png");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 2 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++)
            {
                message = chatWindows[i].GetMessagesUsingClipboard();
                buffer.Clear();
                for (int j = 0; j < message.Length; j++) buffer.Append($"메시지 유형 : {message[j].Type}, 유저 이름 : {message[j].UserName}, 내용 : {message[j].Content} + \n");
                chatWindows[i].SendText(buffer.ToString());
            }
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon1);
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 3 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendImageUsingClipboard(@"res\images\오버액션토끼.png");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 4 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon2);
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText("텍스트 5 / 5");
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++)
            {
                message = chatWindows[i].GetMessagesUsingClipboard();
                buffer.Clear();
                for (int j = 0; j < message.Length; j++) buffer.Append($"메시지 ({j+1} / {message.Length})  유형 : {message[j].Type}, 유저 이름 : {message[j].UserName}, 내용 : {message[j].Content} + \n");
                chatWindows[i].SendText(buffer.ToString());
            }
            Thread.Sleep(1000);
            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendEmoticon(emoticon3);
            Thread.Sleep(1000);

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].SendText($"카카오봇 알파 버전의 모든 기능에 대한 테스트를 종료합니다. (Number : {i + 1}/{chatWindows.Count}, RoomName : {chatWindows[i].RoomName})");
            Thread.Sleep(5000);

            for (int i = 0; i < chatWindows.Count; i++) chatWindows[i].Dispose();

            Console.WriteLine("FinalTest 완료");
        }
    }
}
