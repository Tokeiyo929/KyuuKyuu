# 急救虚拟仿真项目

## 项目简介

本项目是一个基于Unity3D开发的急救虚拟仿真系统，旨在通过高度互动的虚拟训练帮助用户掌握关键急救技能。系统精准模拟了AED使用、崴脚应急处理和抽筋缓解三大常见急救场景，并提供分步骤的专业引导。

本项目运用了**HandUI**组件、**LineRenderer**组件和**空间UI视频播放系统**，通过这些功能模块展示了如何构建直观的交互式用户界面和动态可视化效果。

## 核心技术

### **HandUI 组件**

![pic1](ReadMe/HandUI.gif "w")

- 专为显示急救步骤文本提示而设计的交互组件
- 集成**DOTween**动画系统，实现文本的平滑淡入淡出效果
- 通过结构化`List<StepData>`数据管理，支持用户逐步查看和执行急救任务
- 学习运用`CanvasGroup`组件动态控制UI元素透明度

### **LineRenderer 动态连线工具**

![LineRenderer效果展示](ReadMe/LineRenderer.gif)

- 利用**LineRenderer**组件实现两点间动态连线，实时更新位置数据
- 采用`ExecuteInEditMode`属性，支持编辑模式下实时预览，极大提升开发效率
- 通过transform.LookAt()函数确保连线末端标识始终面向玩家视角

### **空间UI视频教学系统**

![LineRenderer效果展示](ReadMe/VideoPlay.gif)

- 集成**VRUI Package**和**UI Kit for visionOS**，构建悬浮视频播放面板

- 实现交互控制，用户可通过手柄射线进行视频播放、暂停和进度调整

- 自适应视野调节，根据用户位置智能调整UI大小和朝向
