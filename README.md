BabySite
这是一款简单的 demo 游戏。2018 年 厦门 GJ 的产物，我花了点事件将它完善出来了，但是仍然有很多不足的地方。
![1](./resPic/1.png)

1. 遗留的很严重的 bug
当 slider finished 的时候角色一定会从 working 状态切换回 idle 状态导致用户可以进行移动，应该加以条件限制，判断当前 slider 是否为执行成功内容。