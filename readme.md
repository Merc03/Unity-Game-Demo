# Unity Game Demo

by: Mercury03

## 开发日志

### #1 

Day1

学习Git和Unity的基本使用，复习C#语法。尝试创建Unity2D项目并托管到Github仓库。

### #2

Day2

用AddForce初步实现了玩家小球的移动，但是移动曲线不够理想，不能实现悬浮。

todo：参考 [这个帖子](https://www.reddit.com/r/Unity3D/comments/5yqxku/why_does_using_addforce_to_counter_gravity_seem/) 重构小球的升力算法。

### #3

Day3

初步实现黄色光点的的随机生成，以及其与玩家的交互。

基本理解Unity的逻辑，尝试列出to-do list，整理开发计划。

### #4

Day4

开始进行*时间管理*。

初步实现能量-吸收半径的机制，为后续开发做铺垫。

尝试oop，然后重构代码~~（好爽）~~，开发效率大大提高了。

收获巨大！

~~顺便纪念第一次Unity客户端崩溃~~

实现了黑色光点的生成、追踪（但是芝诺的乌龟）。

基本理解了Unity脚本怎么跑。

### #5

Day5

~~胡适你在干什么啊胡适~~

随便写了点黑色光点合成，然后准备搞游戏流程，做点文字提示。

摸鱼ing…明天继续。

### #6

~~如果一个需求不能实现，就简化它~~

实现了白色、粉色光点的基本功能。

感觉开发进度严重滞后了。

加入了不会自发移动的黑色光点。

### #7

~~Win11更新把输入法搞炸了，修系统先~~

调整光点生成算法。

怠惰ing…

用正态分布+一些奇技淫巧实现基本难度递增。

算是完成了核心功能？

## To-do List

-   [x] 完善玩家能量的变化机制
-   [x] 整理屎山，重构粒子生成的框架
-   [x] 加入蓝色光点
-   [x] 加入黑色光点
-   [x] 实现黑色光点的合成机制
-   [x] 实现游戏开始/结束，搭建游戏流程
-   [x] 加入白色光点
-   [x] 加入粉色光点
-   [x] 加入另一种黑色光点（灰色光点），黄点随机转化为任意一种
-   [ ] 整理屎山
    -   [ ] 将计时代码改为Invoke()
-   [x] 重构光点的生成算法，实现难度递增
-   [ ] 重构光点的运动算法，改善观感。
    -   [ ] 黑色光点轨迹重写
    -   [ ] 黑色光点主动合并机制
    -   [ ] 白色光点索敌重写
-   [ ] 重构主角的升力算法，改善手感
-   [ ] 加入运镜机制
-   [ ] [Optional] 加入（三角形的）植被/河流
-   [ ] 数值调整
-   [ ] 整理屎山