using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

// 将两个浮点值相加的作业
public struct MyJobADD : IJob
{
    public float a;//拷贝进来的待处理数据
    public float b;//拷贝进来的待处理数据
    public NativeArray<float> result;//工作结果的引用
    //接口必须实现的方法
    public void Execute()
    {
        result[0] = a + b;
    }
}
//将两个浮点数相乘的作业
public struct MyJobMul : IJob
{
    public float a;//拷贝进来的待处理数据
    public float b;//拷贝进来的待处理数据
    public NativeArray<float> result;//工作结果的引用
    //接口必须实现的方法
    public void Execute()
    {
        result[0] = a * b;
    }
}
