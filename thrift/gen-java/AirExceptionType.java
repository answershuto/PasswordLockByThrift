/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

import java.util.Map;
import java.util.HashMap;
import org.apache.thrift.TEnum;

/**
 * 错误类型
 */
public enum AirExceptionType implements org.apache.thrift.TEnum {
  PASSWD_EXCEPTION(0),
  USER_DIONLINE(1);

  private final int value;

  private AirExceptionType(int value) {
    this.value = value;
  }

  /**
   * Get the integer value of this enum value, as defined in the Thrift IDL.
   */
  public int getValue() {
    return value;
  }

  /**
   * Find a the enum type by its integer value, as defined in the Thrift IDL.
   * @return null if the value is not found.
   */
  public static AirExceptionType findByValue(int value) { 
    switch (value) {
      case 0:
        return PASSWD_EXCEPTION;
      case 1:
        return USER_DIONLINE;
      default:
        return null;
    }
  }
}
