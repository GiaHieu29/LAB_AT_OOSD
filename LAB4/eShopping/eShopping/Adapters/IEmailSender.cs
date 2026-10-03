using System.Collections.Generic;

public interface IEmailSender
{
    void Send(string to, string subject, string body);
}

public class MockEmailSender : IEmailSender
{
    public List<string> Sent = new List<string>();   // test kiểm tra nội dung (BR12)
    public void Send(string to, string subject, string body)
    {
        Sent.Add("TO: " + to + "\r\nSUBJECT: " + subject + "\r\n\r\n" + body);
    }
}