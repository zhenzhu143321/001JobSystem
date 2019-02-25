using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 主线程用来保存工作结果的安全系统数据
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

        // 创建一个相加的作业
        MyJobADD jobData = new MyJobADD();
        jobData.a = 10;//拷贝处理数据到作业中
        jobData.b = 10;//拷贝处理数据到作业中
        jobData.result = result;//把作业中的结果引用指向主线程中的结果数据
        // 调度本作业
        JobHandle handle = jobData.Schedule();

        //确保工作已完成
        //JobSystem自动优先处理作业及其任何依赖项，以便在队列中首先运行，然后尝试
        //在调用Complete函数的线程上执行作业本身。

        //handle.Complete();
        //InvalidOperationException：先前安排的作业MyJobADD写入NativeArray 
        //MyJobADD.result。您必须在作业MyJobADD上调用JobHandle.Complete（），
        //然后才能安全地从NativeArray中读取。
        //创建一个相乘的作业,将上面工作处理的结果再乘10求出结果
        MyJobMul jobData1 = new MyJobMul();
        jobData1.a = 10;//拷贝处理数据到作业中
        jobData1.b = result[0];//拷贝处理数据到作业中
        jobData1.result = result;//把作业中的结果引用指向主线程中的结果数据
        // 调度本作业
        JobHandle handle1 = jobData1.Schedule();
        handle1.Complete();

        //把作业处理后的结果拷贝到主线程变量中
        float aPlusB = result[0];

        //释放分配的内存
        result.Dispose();
        Debug.Log(aPlusB);
    }

   
}
