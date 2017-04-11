using UnityEngine;

public interface IFileReceiver
{
    GameObject ReceiveFile(string filepath);
}

public class FileFinder : MonoBehaviour
{
    public string m_textPath;

    private string m_selectPattern;
    private string m_buttonText;
    private IFileReceiver m_fileReceiver;
    private float m_x;
    private float m_y;

    protected FileBrowser m_fileBrowser;

    [SerializeField]
    protected Texture2D m_directoryImage,
                        m_fileImage;

    // Instatiate object with specific 
    public FileFinder Initialise(string selectPattern, string buttonText, IFileReceiver fileReceiver, float x, float y)
    {
        m_selectPattern = selectPattern;
        m_buttonText = buttonText;
        m_fileReceiver = fileReceiver;
        m_x = x;
        m_y = y;
        return this;
    }

    protected void OnGUI()
    {
        if (m_fileBrowser != null)
        {
            m_fileBrowser.OnGUI();
        }
        else
        {
            OnGUIMain();    
        }
    }

    protected void OnGUIMain()
    {
        if (GUI.Button(new Rect(m_x, m_y, 100, 30), m_buttonText))
            {
            m_fileBrowser = new FileBrowser(
                new Rect(100, 100, 600, 500),
                "Select a File",
                FileSelectedCallback
            );
            m_fileBrowser.SelectionPattern = m_selectPattern;
            m_fileBrowser.DirectoryImage = m_directoryImage;
            m_fileBrowser.FileImage = m_fileImage;
        }
    }

    protected void FileSelectedCallback(string path)
    {
        if(m_fileReceiver == null)
        {
            Debug.Log("Null file receiver");
        }
        m_fileBrowser = null;
        m_textPath = path;
        if(m_textPath != null)
            m_fileReceiver.ReceiveFile(m_textPath);
    }
}