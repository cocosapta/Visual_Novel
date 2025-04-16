using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    public static string NamaScene;

    public TextAsset assetSoal;
    private string[]    soal;
    private string[,] soalBag;

    int indexSoal;
    int maxSoal;
    bool ambilSoal;
    char kunciJ;

    bool[] soalSelesai;

    public TextMeshProUGUI txtSoal, txtOpsiA, txtOpsiB, txtOpsiC, txtOpsiD;
    bool isHasil;
    private float durasi;
    public float durasiPenilaian;

    int jwbBenar, jwbSalah;
    float nilai;

    public GameObject panel;
    public GameObject imgHasil;
    public GameObject ulangi;
    public GameObject lanjut;
    public TextMeshProUGUI txtHasil, txtTotal;

    void Start()
    {
        if (assetSoal == null || string.IsNullOrEmpty(assetSoal.text))
        {
            Debug.LogError("Asset soal kosong atau tidak ditemukan.");
            return;
        }

        durasi = durasiPenilaian;
        soal = assetSoal.text.Split('#');

        if (soal.Length == 0)
        {
            Debug.LogError("Data soal kosong. Pastikan file assetSoal memiliki data yang valid.");
            return;
        }

        soalSelesai = new bool[soal.Length];
        soalBag = new string[soal.Length, 6];
        maxSoal = soal.Length;

        OlahSoal();
        ambilSoal = true;
        TampilanSoal();
    }

    public void OlahSoal()
    {
        for (int i = 0; i < soal.Length; i++)
        {
            string[] tempSoal = soal[i].Split('+');
            for (int j = 0; j < tempSoal.Length; j++)
            {
                soalBag[i, j] = tempSoal[j];
                continue;
            }
            continue;
        }
    }

    public void TampilanSoal()
    {
        if (indexSoal < maxSoal)
        {
            if (ambilSoal)
            {
                for (int i = 0; i < soal.Length; i++)
                {
                    int randomIndexSoal = Random.Range(0, soal.Length);
                    if (!soalSelesai[randomIndexSoal])
                    {
                        txtSoal.text = soalBag[randomIndexSoal, 0];
                        txtOpsiA.text = soalBag[randomIndexSoal, 1];
                        txtOpsiB.text = soalBag[randomIndexSoal, 2];
                        txtOpsiC.text = soalBag[randomIndexSoal, 3];
                        txtOpsiD.text = soalBag[randomIndexSoal, 4];

                        kunciJ = soalBag[randomIndexSoal, 5][0];
                        soalSelesai[randomIndexSoal] = true;
                        ambilSoal = false;
                        break;
                    }
                }
            }
        }
    }

    public void Opsi(string opsiHuruf)
    {
        CheckJawaban(opsiHuruf[0]);
        if (indexSoal == maxSoal - 1)
        {
            isHasil = true;
        }
        else
        {
            indexSoal++;
            ambilSoal = true;
        }
        panel.SetActive(true);
    }

    private float HitungNilai()
    {
        return nilai = (float)jwbBenar / maxSoal * 100;
    }

    public void CheckJawaban(char huruf)
    {
        if (huruf.Equals(kunciJ))
        {
            jwbBenar++;
        }
        else
        {
            jwbSalah++;
        }
    }

    public void btn_ulang(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
    }

    void Update()
    {
        if (panel.activeSelf)
        {
            durasiPenilaian -= Time.deltaTime;

            if (isHasil)
            {
                
                if (durasiPenilaian <= 0)
                {
                    imgHasil.SetActive(true);
                    txtHasil.text = "Benar : " + jwbBenar + "\nSalah : " + jwbSalah;
                    txtTotal.text = " " + HitungNilai();
                    durasiPenilaian = 0;
                    if(HitungNilai() <= 70)
                    {
                        ulangi.SetActive(true);
                        lanjut.SetActive(false);
                    }
                    else
                    {
                        ulangi.SetActive(false);
                        lanjut.SetActive(true);

                    }
                }
            }
            else
            {
                if (durasiPenilaian <= 0)
                {
                    panel.SetActive(false);
                    durasiPenilaian = durasi;
                    TampilanSoal();
                }
            }
        }
    }
}
