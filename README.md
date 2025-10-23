# Game Demo NINJA FROG
## Function note
### Prefabs
- Các gameObject đã hoàn thiện và có thể tái sử dụng
### Camera
- **CameraController.cs**: Xử lý camera follow theo người chơi
### Fruits
- **FruitsController.cs**: Xử lý animation khi người chơi va chạm
### Player
- **FollowPlayerUI.cs**: Xử lý thanh máu di chuyển theo nhân vật, thanh máu không được lật trái phải theo nhân vật  
- **Health.cs**: Xử lý thay đổi máu của nhận vật  
- **PlayerCollision.cs**: Xử lý va chạm giữa nhân vật với các thành khác trong game  
- **PlayerController.cs**: Xử lý logic di chuyển của nhân vật  
### Traps
- **FireTrap.cs**: Xử lý bẫy lửa hoạt động, gây sát thương cho nhân vật  
- **Platform.cs**: Xử lý platform di chuyển  
- **RockHead.cs**: Xử lý rock head rơi xuống khi thấy người chơi đi qua  
- **Saw.cs**: Xử lý di chuyển giữa 2 điểm theo trục x  
- **SpikedBall.cs**: Xử lý xoay tròn theo trục z và lăn dần theo trục x  
- **SpikeHead.cs**: Tạo mảng Vector3 với 4 hướng, mỗi hướng tạo 1 raycast với độ dài range để phát hiện người chơi, vòng lặp for để lần lượt bắn ra 4 tia ở 4 hướng  
- **Trampoline.cs**: Tạo boxcast với khoảng cách nhỏ kiểm tra người chơi đứng trên trampoline thì đẩy người chơi lên
## Game chỉ mới có các thành phần game object riêng lẻ, chưa hoàn thiện ạ
