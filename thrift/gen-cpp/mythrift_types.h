/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
#ifndef mythrift_TYPES_H
#define mythrift_TYPES_H

#include <thrift/Thrift.h>
#include <thrift/TApplicationException.h>
#include <thrift/protocol/TProtocol.h>
#include <thrift/transport/TTransport.h>

#include <thrift/cxxfunctional.h>




struct AirExceptionType {
  enum type {
    PASSWD_EXCEPTION = 0,
    USER_DIONLINE = 1
  };
};

extern const std::map<int, const char*> _AirExceptionType_VALUES_TO_NAMES;


class AirException : public ::apache::thrift::TException {
 public:

  static const char* ascii_fingerprint; // = "896B057104D7399B13F265CB3CF2EAA6";
  static const uint8_t binary_fingerprint[16]; // = {0x89,0x6B,0x05,0x71,0x04,0xD7,0x39,0x9B,0x13,0xF2,0x65,0xCB,0x3C,0xF2,0xEA,0xA6};

  AirException() : type((AirExceptionType::type)0), errorCode(0), errorMessage(), debugMessage() {
  }

  virtual ~AirException() throw() {}

  AirExceptionType::type type;
  int64_t errorCode;
  std::string errorMessage;
  std::string debugMessage;

  void __set_type(const AirExceptionType::type val) {
    type = val;
  }

  void __set_errorCode(const int64_t val) {
    errorCode = val;
  }

  void __set_errorMessage(const std::string& val) {
    errorMessage = val;
  }

  void __set_debugMessage(const std::string& val) {
    debugMessage = val;
  }

  bool operator == (const AirException & rhs) const
  {
    if (!(type == rhs.type))
      return false;
    if (!(errorCode == rhs.errorCode))
      return false;
    if (!(errorMessage == rhs.errorMessage))
      return false;
    if (!(debugMessage == rhs.debugMessage))
      return false;
    return true;
  }
  bool operator != (const AirException &rhs) const {
    return !(*this == rhs);
  }

  bool operator < (const AirException & ) const;

  uint32_t read(::apache::thrift::protocol::TProtocol* iprot);
  uint32_t write(::apache::thrift::protocol::TProtocol* oprot) const;

};

void swap(AirException &a, AirException &b);



#endif
