using System.Drawing;
using System;
using System.Runtime.InteropServices;
using Less.API.NetFramework.WindowsAPI;

// .Net Framework에서 기본 제공하는 Clipboard 클래스는 불안정하기 때문에, 전부 Native API로 처리해야 함.

namespace Less
{
    namespace API
    {
        namespace NetFramework
        {
            namespace ClipboardManagerAPI
            {
                public sealed class ClipboardManager
                {
                    /// <summary>
                    /// TODO : 이 메서드의 내용을 정의해야 합니다.
                    /// </summary>
                    public static void BackupData()
                    {

                    }

                    /// <summary>
                    /// TODO : 이 메서드의 내용을 정의해야 합니다.
                    /// </summary>
                    public static void RestoreData()
                    {

                    }

                    /// <summary>
                    /// 클립보드에서 텍스트를 가져옵니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생하고, 만약 텍스트가 존재하지 않을 경우 null을 반환합니다.
                    /// </summary>
                    public static string GetText()
                    {
                        string text = null;

                        bool isClipboardOpen = Windows.OpenClipboard(IntPtr.Zero);
                        if (!isClipboardOpen) throw new CannotOpenException();
                        IntPtr pointer = Windows.GetClipboardData(Windows.CF_UNICODETEXT);
                        if (pointer == IntPtr.Zero)
                        {
                            pointer = Windows.GetClipboardData(Windows.CF_TEXT);
                            if (pointer != IntPtr.Zero) text = Marshal.PtrToStringAnsi(pointer);
                        }
                        else text = Marshal.PtrToStringUni(pointer);
                        Windows.CloseClipboard();

                        return text;
                    }

                    /// <summary>
                    /// 클립보드에 텍스트를 저장합니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    /// </summary>
                    /// <param name="text">저장할 텍스트</param>
                    public static void SetText(string text)
                    {
                        bool isClipboardOpen = Windows.OpenClipboard(IntPtr.Zero);
                        if (!isClipboardOpen) throw new CannotOpenException();
                        Windows.EmptyClipboard();
                        Windows.SetClipboardData(Windows.CF_TEXT, Marshal.StringToHGlobalAnsi(text));
                        Windows.SetClipboardData(Windows.CF_UNICODETEXT, Marshal.StringToHGlobalUni(text));
                        Windows.CloseClipboard();
                    }

                    /// <summary>
                    /// 클립보드에 이미지를 저장합니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    /// </summary>
                    /// <param name="imagePath">저장할 이미지의 원본 파일 경로</param>
                    public static void SetImage(string imagePath)
                    {
                        using (Bitmap image = (Bitmap)Image.FromFile(imagePath))
                        {
                            using (Graphics graphics = Graphics.FromImage(image))
                            {
                                IntPtr hScreenDC = Windows.GetWindowDC(IntPtr.Zero); // 기본적인 Device Context의 속성들을 카피하기 위한 작업
                                IntPtr hDestDC = Windows.CreateCompatibleDC(hScreenDC);
                                IntPtr hDestBitmap = Windows.CreateCompatibleBitmap(hScreenDC, image.Width, image.Height); // destDC와 destBitmap 모두 반드시 screenDC의 속성들을 기반으로 해야 함.
                                IntPtr hPrevDestObject = Windows.SelectObject(hDestDC, hDestBitmap);

                                IntPtr hSourceDC = graphics.GetHdc();
                                IntPtr hSourceBitmap = image.GetHbitmap();
                                IntPtr hPrevSourceObject = Windows.SelectObject(hSourceDC, hSourceBitmap);

                                Windows.BitBlt(hDestDC, 0, 0, image.Width, image.Height, hSourceDC, 0, 0, Windows.SRCCOPY);

                                Windows.DeleteObject(Windows.SelectObject(hSourceDC, hPrevSourceObject));
                                Windows.SelectObject(hDestDC, hPrevDestObject); // 리턴값 : hDestBitmap
                                graphics.ReleaseHdc(hSourceDC);
                                Windows.DeleteDC(hDestDC);

                                bool isClipboardOpen = Windows.OpenClipboard(IntPtr.Zero);
                                if (!isClipboardOpen)
                                {
                                    Windows.DeleteObject(hDestBitmap);
                                    throw new CannotOpenException();
                                }
                                Windows.EmptyClipboard();
                                Windows.SetClipboardData(Windows.CF_BITMAP, hDestBitmap);
                                Windows.CloseClipboard();

                                Windows.DeleteObject(hDestBitmap);
                            }
                        }
                    }

                    public class CannotOpenException : Exception
                    {
                        internal CannotOpenException() : base("클립보드가 다른 프로그램에 의해 이미 사용되고 있습니다.") { }
                    }

                    public class InvalidFormatRequestException : Exception
                    {
                        internal InvalidFormatRequestException() : base("잘못된 클립보드 포맷 요청입니다.") { }
                    }

                    // Legacy Codes

                    ///// <summary>
                    ///// 현재 클립보드에 저장되어 있는 데이터를 백업합니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    ///// </summary>
                    //public static void BackupData()
                    //{
                    //    IDataObject source = null;
                    //    try { source = Clipboard.GetDataObject(); }
                    //    catch (ExternalException e) { throw new CannotOpenException(); }
                    //    formats = source.GetFormats();
                    //    if (formats.Length == 0) return;
                    //    dataList.Clear();
                    //    foreach (string t in formats) dataList.Add(source.GetData(t));
                    //}

                    ///// <summary>
                    ///// 백업했던 클립보드 데이터를 복구합니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    ///// </summary>
                    //public static void RestoreData()
                    //{
                    //    if (formats == null || formats.Length == 0) return;
                    //    var result = new DataObject();
                    //    for (int i = 0; i < dataList.Count; i++) result.SetData(formats[i], dataList[i]);
                    //    try { Clipboard.SetDataObject(result, true); }
                    //    catch (ExternalException e) { throw new CannotOpenException(); }
                    //}

                    ///// <summary>
                    ///// 클립보드에 저장된 텍스트를 가져옵니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    ///// </summary>
                    //public static string GetText()
                    //{
                    //    string text;
                    //    try { text = Clipboard.GetText(); }
                    //    catch (ExternalException e) { throw new CannotOpenException(); }

                    //    return Clipboard.GetText();
                    //}

                    ///// <summary>
                    ///// 클립보드에 이미지를 저장합니다. 클립보드 열기 요청 실패 시 ClipboardManager.CannotOpenException 예외가 발생합니다.
                    ///// </summary>
                    ///// <param name="filePath"></param>
                    //public static void SetImage(string filePath)
                    //{
                    //    using (var image = Image.FromFile(filePath))
                    //    {
                    //        try { Clipboard.SetImage(image); }
                    //        catch (ExternalException e) { throw new CannotOpenException(); }
                    //    }
                    //}
                }
            }
        }
    }
}