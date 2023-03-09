using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class NetworkImage : MonoBehaviour
{
  [SerializeField] private string _imageUrl = "https://picsum.photos/512/512";
  [SerializeField] private TMP_InputField _inputField;

  // Components.
  private RawImage _rawImage;
  private Image _image;
  private SpriteRenderer _spriteRenderer;

  private void Awake()
  {
    _rawImage = GetComponent<RawImage>();
    _image = GetComponent<Image>();
    _spriteRenderer = GetComponent<SpriteRenderer>();
  }

  public void Load()
  {
    string text = _inputField?.text;
    string url = !string.IsNullOrEmpty(text) ? text : _imageUrl;
    StartCoroutine(LoadImage(url));
  }

  private IEnumerator LoadImage(string imageUrl)
  {
    UnityWebRequest req = UnityWebRequestTexture.GetTexture(imageUrl);

    // 画像ができるまで待つ
    yield return req.SendWebRequest();
    if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
    {
      Debug.Log(req.error);
      yield break;
    }

    Texture2D tex = ((DownloadHandlerTexture)req.downloadHandler).texture;
    if (_rawImage != null)
    {
      _rawImage.texture = tex;
    }
    else if (_image != null || _spriteRenderer != null)
    {
      Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
      if (_image != null)
      {
        _image.sprite = sprite;
      }
      else if (_spriteRenderer != null)
      {
        _spriteRenderer.sprite = sprite;
      }
    }
  }
}
