using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // 1. KHAI BÁO BIẾN Ở ĐÂY
    // Khai báo một biến private kiểu Animator tên là _anim
    private Animator _anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2. SỬA LỖI CHÍNH TẢ
        // Bây giờ bạn có thể gán giá trị cho biến _anim đã được khai báo
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public void Move(float move)
    {
        // Sử dụng biến _anim để điều khiển Animator
        _anim.SetFloat("Move", Mathf.Abs(move));
    }
}