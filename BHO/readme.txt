install
C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase "BHO.dll"
uninstall
C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe /codebase "BHO.dll" /u

https://www.cnblogs.com/mvc2014/p/3776054.html

js注入
//找到head元素
HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
//创建script标签
HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
//给script标签加js内容
element.text = "function sayHello() { alert('hello') }";
//将script标签添加到head标签中
head.AppendChild(scriptEl);
//执行js代码
webBrowser1.Document.InvokeScript("sayHello");

voidwebBrowser1_Navigating(objectsender,WebBrowserNavigatingEventArgse)
　　　　{
　　　　　　if(e.Url.ToString().ToLower().Trim('/')=="cmd://onmousedown")
　　　　　　{
　　　　　　　　MessageBox.Show("jinjazz路过");
　　　　　　　　e.Cancel=true;
　　　　　　}
　　　　}
　　　　voidwb_NavigateComplete2(objectpDisp,refobjectURL)
　　　　{
　　　　　　mshtml.IHTMLDocument2doc=(this.webBrowser1.ActiveXInstanceasSHDocVw.WebBrowser).Documentasmshtml.IHTMLDocument2;
　　　　　　doc.parentWindow.execScript("document.onmousedown=function(e){window.location='cmd://onmousedown'}","javascript");
　　　　}


            //try
            //{
            //    document = (HTMLDocument)webBrowser.Document;
            //    foreach (IHTMLInputElement element in document.getElementsByTagName("INPUT"))
            //    {
            //        MessageBox.Show(element.name != null? element.name : "no name");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //            MessageBox.Show("OnDocumentComplete");

			  //document = (HTMLDocument)webBrowser.Document;
            //foreach (IHTMLInputElement element in document.getElementsByTagName("INPUT"))
            //{
            //    if (element.name.ToLower().Equals("go"))
            //    {
            //        Form1 login = new Form1();
            //        login.ShowDialog();
            //    }
            //}