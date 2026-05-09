# pong
Game Practice Demo--Pong


## 🎯 游戏说明

### 操作方式

| 玩家 | 上移 | 下移 |
|------|------|------|
| 左侧 | W | S |
| 右侧 | ↑ | ↓ |

### 游戏规则

- 球进入对方球门时得分
- 达到 **5 分** 即获胜
- 游戏将暂停并显示胜利信息

## 🚀 运行方式

1. 确保已安装 [Godot 4.x](https://godotengine.org/download) 引擎
2. 使用 Godot 打开项目文件夹
3. 点击运行按钮或按 `F5` 启动游戏

## 📝 核心代码说明

### 碰撞检测（Area2D.BodyEntered）

游戏使用 `Area2D` 节点检测球是否进入球门区域：

```csharp
rightGoal.BodyEntered += body => ScorePoint(body, Player.Left);
```

当球进入右边的球门时，触发 `ScorePoint` 方法，左侧玩家得分。

### 球拍控制

球拍通过读取键盘输入控制移动：

```csharp
if (Input.IsActionPressed(UpKey)){
    direction -= 1.0f;
}
```

### 球的碰撞反弹

球根据碰撞对象改变运动方向：

```csharp
if (collision != null)
{
    if (collider.IsInGroup("wall_up") || collider.IsInGroup("wall_down")){
        HitWall();  // 上下墙壁反弹
    }
    else if (collider.IsInGroup("paddle_left") || collider.IsInGroup("paddle_right")){
        HitPaddle(); // 球拍反弹
    }
}
```

## 📜 许可证

本项目基于 MIT 许可证开源。