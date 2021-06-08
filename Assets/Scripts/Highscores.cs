using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour
{
	private string privateCode;
	private string publicCode;
	const string webURL = "http://dreamlo.com/lb/";

	DisplayHighscores highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;
	public int change;

	public void Awake()
	{
		if(PlayerPrefsSafe.GetInt("Level") == 30 || change == 1)
		{
			privateCode = "v67D7gFr90S2xGDucHEAlgE94x0UPRCk-6kggbFGklgg";
			publicCode = "5e77bc67fe232612b8d0313e";
		}
		else if(PlayerPrefsSafe.GetInt("Level") == 50 || change == 2)
		{
			privateCode = "7Evj15txW06UXGLi1F9kxAmxvDXKY8BUmOZuOPiiMuSw";
			publicCode = "5e7a1e47fe232612b8d9050a";
		}
		else if(PlayerPrefsSafe.GetInt("Level") == 100 || change == 3)
		{
			privateCode = "E8cLhQRW40GYVTOElcPaOwBiv5O0M-qEyv4gj_n85_Pg";
			publicCode = "5e7be83bfe232612b8dfe2d4";
		}
		highscoreDisplay = GetComponent<DisplayHighscores>();
		instance = this;
	}

	public void AddNewHighscore(string username, int score)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful");
			DownloadHighscores();
		}
		else
		{
			print("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
			highscoreDisplay.OnHighscoresDownloaded(highscoresList);
		}
		else
		{
			print("Error Downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
			print(highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}

}

public struct Highscore
{
	public string username;
	public int score;

	public Highscore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}
}