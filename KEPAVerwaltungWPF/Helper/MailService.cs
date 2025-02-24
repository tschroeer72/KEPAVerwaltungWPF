using System.Net.Mail;

namespace KEPAVerwaltungWPF.Helper;

public class MailService
{
    private string _strFrom;
    private List<string> _lstTo;
    private List<string> _lstCC;
    private List<string> _lstBCC;
    private List<Attachment> _lstAttachment;
    private string _strSubject;
    private string _strBody;
    private string _strSMTPUser;
    private string _strSMTPPassword;
    private string _strSMTPServer;
    private Int32 _intSMTPPort;
    private Boolean _bolTLS;

    /// <summary>
    /// Stellt die Mail-Adresse des Absenders dar
    /// </summary>        
    public string From {
        get {
            return _strFrom;
        }
        set {
            _strFrom = value;
        }
    }

    /// <summary>
    /// Stellt eine Auflistung der Empfänger dar
    /// </summary>
    public List<string> To {
        get {
            return _lstTo;
        }
        set {
            _lstTo = value;
        }
    }

    /// <summary>
    /// Stellt eine Auflistung der Empfänger dar, die die Mail als Kopie erhalten
    /// </summary>
    public List<string> CC {
        get {
            return _lstCC;
        }
        set {
            _lstCC = value;
        }
    }

    /// <summary>
    /// Stellt eine Auflistung der Empfänger dar, die die Mail als Blindkopie erhalten
    /// </summary>
    public List<string> BCC {
        get {
            return _lstBCC;
        }
        set {
            _lstBCC = value;
        }
    }

    /// <summary>
    /// Stellt eine Auflistung von den Dateinamen der Anhänge dar
    /// </summary>
    public List<Attachment> Attachment {
        get {
            return _lstAttachment;
        }
        set {
            _lstAttachment = value;
        }
    }

    /// <summary>
    /// Stellt den Betreff der Mail dar
    /// </summary>
    public string Subject {
        get {
            return _strSubject;
        }
        set {
            _strSubject = value;
        }
    }

    /// <summary>
    /// Stellt den Text der Nachricht dar
    /// </summary>
    public string Body {
        get {
            return _strBody;
        }
        set {
            _strBody = value;
        }
    }

    /// <summary>
    /// Stellt den Usernamen dar
    /// </summary>  
    public string SMTPUser {
        get {
            return _strSMTPUser;
        }
        set {
            _strSMTPUser = value;
        }
    }

    /// <summary>
    /// Stellt das Passwort dar
    /// </summary>
    public string SMTPPassword {
        get {
            return _strSMTPPassword;
        }
        set {
            _strSMTPPassword = value;
        }
    }

    /// <summary>
    /// Stellt den Namen des SMTP-Servers dar
    /// </summary>
    public string SMTPServer {
        get {
            return _strSMTPServer;
        }
        set {
            _strSMTPServer = value;
        }
    }

    /// <summary>
    /// Stellt den Port für die Mail-Übermittlung dar
    /// </summary>
    public Int32 SMTPPort {
        get {
            return _intSMTPPort;
        }
        set {
            _intSMTPPort = value;
        }
    }

    /// <summary>
    /// TLS aktivieren
    /// </summary>
    public Boolean TLS {
        get {
            return _bolTLS;
        }
        set {
            _bolTLS = value;
        }
    }

    public MailService()
    {
        _strFrom = string.Empty;
        _lstTo = new List<string>();
        _lstCC = new List<string>();
        _lstBCC = new List<string>();
        _lstAttachment = new List<Attachment>();
        _strSubject = string.Empty;
        _strBody = string.Empty;
        _strSMTPUser = string.Empty;
        _strSMTPPassword = string.Empty;
        _strSMTPServer = string.Empty;
        _intSMTPPort = -1;
        _bolTLS = false;
    }


    /// <summary>
    /// Versendet die Mail mit den festgelegten Eigenschaften in der Klasse
    /// </summary>
    public void Send()
    {
        MailMessage mmEmail = new MailMessage();

        MailAddress maSender = new MailAddress(_strFrom);
        mmEmail.From = maSender; // Absender einstellen

        // Empfänger hinzufügen
        foreach (string empf in _lstTo)
            mmEmail.To.Add(empf);

        // Kopie-Empfänger hinzufügen (wenn vorhanden)
        if (_lstCC.Count != 0)
            foreach (string kopie in _lstCC)
                mmEmail.CC.Add(kopie);

        // Blindkopie-Empfänger hinzufügen (wenn vorhanden)
        if (_lstBCC.Count != 0)
            foreach (string blindkopie in _lstBCC)
                mmEmail.Bcc.Add(blindkopie);

        // Anhänge hinzufügen (wenn vorhanden)
        if (_lstAttachment.Count != 0)
            foreach (Attachment anhang in _lstAttachment)
                mmEmail.Attachments.Add(anhang);

        mmEmail.Subject = _strSubject; // Betreff hinzufügen

        mmEmail.Body = _strBody; // Nachrichtentext hinzufügen

        SmtpClient smtpMailClient = new SmtpClient(_strSMTPServer, _intSMTPPort); // Postausgangsserver definieren

        string strUserName = _strSMTPUser;
        string strPassword = _strSMTPPassword;
        System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(strUserName, strPassword);

        smtpMailClient.EnableSsl = _bolTLS;
        smtpMailClient.Credentials = Credentials; // Anmeldeinformationen setzen

        smtpMailClient.Send(mmEmail); // mmEmail senden
    }
}