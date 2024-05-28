﻿using XFrame.Core;
using XFrame.Core.Threads;
using XFrame.Modules.Archives;
using XFrame.Modules.Conditions;
using XFrame.Modules.Containers;
using XFrame.Modules.Crypto;
using XFrame.Modules.Datas;
using XFrame.Modules.Diagnotics;
using XFrame.Modules.Download;
using XFrame.Modules.Entities;
using XFrame.Modules.Event;
using XFrame.Modules.ID;
using XFrame.Modules.Local;
using XFrame.Modules.Plots;
using XFrame.Modules.Pools;
using XFrame.Modules.Procedure;
using XFrame.Modules.Rand;
using XFrame.Modules.Reflection;
using XFrame.Modules.Resource;
using XFrame.Modules.Serialize;
using XFrame.Modules.StateMachine;
using XFrame.Modules.Times;
using XFrameShare.Network;

public static partial class Global
{
    public static ITypeModule Type => m_Type ??= Entry.GetModule<ITypeModule>();
    public static IArchiveModule Archive => m_Archive ??= Entry.GetModule<IArchiveModule>();
    public static IConditionModule Condition => m_Condition ??= Entry.GetModule<IConditionModule>();
    public static IContainerModule Container => m_Container ??= Entry.GetModule<IContainerModule>();
    public static ICryptoModule Crypto => m_Crypto ??= Entry.GetModule<ICryptoModule>();
    public static IDataModule Data => m_Data ??= Entry.GetModule<IDataModule>();
    public static IDownloadModule Download => m_Download ??= Entry.GetModule<IDownloadModule>();
    public static IEntityModule Entity => m_Entity ??= Entry.GetModule<IEntityModule>();
    public static IEventModule Event => m_Event ??= Entry.GetModule<IEventModule>();
    public static IFsmModule Fsm => m_Fsm ??= Entry.GetModule<IFsmModule>();
    public static IIdModule Id => m_Id ??= Entry.GetModule<IIdModule>();
    public static ILocalizeModule I18N => m_I18N ??= Entry.GetModule<ILocalizeModule>();
    public static ILogModule Log => m_Log ??= Entry.GetModule<ILogModule>();
    public static IPlotModule Plot => m_Plot ??= Entry.GetModule<IPlotModule>();
    public static IPoolModule Pool => m_Pool ??= Entry.GetModule<IPoolModule>();
    public static IProcedureModule Procedure => m_Procedure ??= Entry.GetModule<IProcedureModule>();
    public static IRandModule Rand => m_Rand ??= Entry.GetModule<IRandModule>();
    public static IResModule Res => m_Res ??= Entry.GetModule<IResModule>();
    public static ISerializeModule Serialize => m_Serialize ??= Entry.GetModule<ISerializeModule>();
    public static ITimeModule Time => m_Time ??= Entry.GetModule<ITimeModule>();
    public static FiberModule Fiber => m_Fiber ??= Entry.GetModule<FiberModule>();
    public static NetworkModule Net => m_Net ??= Entry.GetModule<NetworkModule>();
}