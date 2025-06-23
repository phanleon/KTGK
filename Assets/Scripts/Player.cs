using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Khai bao bien Rigidbody2D 
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;

    [SerializeField] // Cho phep hien thi bien trong Unity Editor
    private float _jumpForce = 5f; // Luu tru luc nhay

    [SerializeField]
    private float _speed = 5.0f; // Toc do di chuyen cua nhan vat

    // ----- CÁC BIẾN BỊ THIẾU MÌNH ĐÃ BỔ SUNG -----
    private bool _resetJump = false; // Biến này bạn đã dùng nhưng chưa khai báo

    [SerializeField]
    private float _groundCheckDistance = 0.6f; // Khoảng cách để kiểm tra đất

    [SerializeField]
    private LayerMask _groundLayer; // Layer của mặt đất (bạn cần thiết lập trong Unity)

    void Start()
    {
        // Lấy component Rigidbody2D một lần khi bắt đầu
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();

        // Thiết lập layer mặt đất bằng tên, ví dụ layer của bạn tên là "Ground"
        _groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Gọi hàm di chuyển
        Movement();

        // Gửi thông tin di chuyển cho script Animation
        // Lấy giá trị tuyệt đối để animator luôn nhận giá trị dương
        _anim.Move(Mathf.Abs(_rigid.linearVelocity.x));
    }

    void Movement()
    {
        // Lấy input di chuyển ngang từ bàn phím (A/D hoặc phím mũi tên)
        float move = Input.GetAxisRaw("Horizontal");

        // Thiết lập vận tốc ngang của Rigidbody, giữ nguyên vận tốc dọc
        _rigid.linearVelocity = new Vector2(move * _speed, _rigid.linearVelocity.y);

        // --- SỬA LỖI Ở ĐÂY ---
        // Kiểm tra nếu nhấn phím Space VÀ kết quả của hàm IsGrounded() là true
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // Dùng AddForce với Impulse để tạo một lực đẩy tức thời
            _rigid.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Bắn một tia Raycast từ vị trí của người chơi xuống dưới
        // 1. Vị trí bắt đầu: transform.position
        // 2. Hướng: Vector2.down (hướng xuống)
        // 3. Khoảng cách: _groundCheckDistance
        // 4. Layer mask: Chỉ kiểm tra va chạm với layer mặt đất (_groundLayer)
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);

        // Vẽ một tia trong Scene view để dễ debug
        Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, Color.green);

        // Nếu hitInfo.collider không phải là null, nghĩa là tia đã va chạm với mặt đất
        if (hitInfo.collider != null)
        {
            // Đoạn code này có thể bạn dùng cho logic double jump sau này, tạm thời mình giữ nguyên
            if (_resetJump == false)
            {
                return true; // Đang ở trên mặt đất
            }
        }

        return false; // Không ở trên mặt đất
    }
}