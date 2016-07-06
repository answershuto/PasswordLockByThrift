

/**
 * 错误类型
 */
enum AirExceptionType {
    PASSWD_EXCEPTION = 0;
	
	USER_DIONLINE = 1;
}

/**
 * 调用错误
 */
exception AirException {
    /** 错误类型 */
    1: required AirExceptionType type;

    /** 错误码 */
    2: required i64 errorCode;

    /** 错误消息 */
    3: required string errorMessage;

    /** 调试信息 */
    4: required string debugMessage;
}





service passwordLock {

	/**
     * 连接服务器,返回Session
     */
	 string connectServer (
		/** 设备编码(加密) */
        1: string strDevCode;	

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );

	/**
     * 与服务器断开连接
     */
	 void disconnectServer (
		/** 用户Session */
        1: string Session;	

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );
	
	
	/**
     * 保活
     */
	 void keepAlive (
		/** 用户Session */
        1: string Session;	

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );

    /**
     * 输入密码
     */
    void inputPasswd (
        /** 用户Session */
        1: string Session;

		/** 锁类型 */
        2: i32 iLockType;
		
        /** 密码 */
        3: string strPassword;

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );
	
	
	/**
     * 重置密码
     */
    void ResetPasswd (
        /** 加密后的设备编码：使得只有某些指定设备才可以进行重置密码，防止黑客通过抓包得到接口并直接调用接口 */
        1: string strDevCode;
		
		/** 锁类型 */
        2: i32 iLockType;

        /** 密码 */
        3: string strPassword;

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );
	
	
	/**
     * 修改密码
     */
    void modifyPasswd (
		/*原有密码*/
		1:string strOldPasswd;
		
		/*新密码*/
		2:string strNewPasswd;
		
		/** 锁类型 */
        3: i32 iLockType;

    ) throws (
        /** 抛出异常 */
        1: AirException ex;
    );
	

	}
