using UnityEngine; // ���������� ������������ ���� UnityEngine ��� ������ � �������� ��������� � ������������
using UnityEngine.UI; // ���������� ������������ ���� ��� ������ � UI ����������, ������ ��� ������

public class InterectableForUstanovki : MonoBehaviour // ���������� �����, ������� ��������� �� MonoBehaviour
{
    // ��������� ������� ��� ��������� ��������������
    public delegate void Interect();

    // ����������� �������, � �������� ����� ������������� ������ ������
    public static Interect InterectForUstanovki;

    // ������ �� ������, ������� ����� ����������������� � �������
    public Button PlatformImageButton;

    private void Start()
    {
        // ������ ������ ���������� ��� ������ ����
        PlatformImageButton.interactable = false;
    }

    private void OnEnable()
    {
        // ������������� �� ������� InterectForUstanovki, ����� �������� ����� inter ��� ��� ������������
        InterectForUstanovki += inter;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������, ����� �������� ������ ������
        InterectForUstanovki -= inter;
    }

    private void inter()
    {
        // ���������, ������ �� �����
        if (PlayerFoundLost.Instance.IsFound)
        {
            // ���� ����� ������, ������ ������ ��������
            PlatformImageButton.interactable = true;
        }
        else
        {
            // ���� ����� �� ������, ������ ������ ����������
            PlatformImageButton.interactable = false;
        }
    }
}