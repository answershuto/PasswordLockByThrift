<?php
namespace ;

/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
use Thrift\Base\TBase;
use Thrift\Type\TType;
use Thrift\Type\TMessageType;
use Thrift\Exception\TException;
use Thrift\Exception\TProtocolException;
use Thrift\Protocol\TProtocol;
use Thrift\Protocol\TBinaryProtocolAccelerated;
use Thrift\Exception\TApplicationException;


final class AirExceptionType {
  const PASSWD_EXCEPTION = 0;
  const USER_DIONLINE = 1;
  static public $__names = array(
    0 => 'PASSWD_EXCEPTION',
    1 => 'USER_DIONLINE',
  );
}

class AirException extends TException {
  static $_TSPEC;

  public $type = null;
  public $errorCode = null;
  public $errorMessage = null;
  public $debugMessage = null;

  public function __construct($vals=null) {
    if (!isset(self::$_TSPEC)) {
      self::$_TSPEC = array(
        1 => array(
          'var' => 'type',
          'type' => TType::I32,
          ),
        2 => array(
          'var' => 'errorCode',
          'type' => TType::I64,
          ),
        3 => array(
          'var' => 'errorMessage',
          'type' => TType::STRING,
          ),
        4 => array(
          'var' => 'debugMessage',
          'type' => TType::STRING,
          ),
        );
    }
    if (is_array($vals)) {
      if (isset($vals['type'])) {
        $this->type = $vals['type'];
      }
      if (isset($vals['errorCode'])) {
        $this->errorCode = $vals['errorCode'];
      }
      if (isset($vals['errorMessage'])) {
        $this->errorMessage = $vals['errorMessage'];
      }
      if (isset($vals['debugMessage'])) {
        $this->debugMessage = $vals['debugMessage'];
      }
    }
  }

  public function getName() {
    return 'AirException';
  }

  public function read($input)
  {
    $xfer = 0;
    $fname = null;
    $ftype = 0;
    $fid = 0;
    $xfer += $input->readStructBegin($fname);
    while (true)
    {
      $xfer += $input->readFieldBegin($fname, $ftype, $fid);
      if ($ftype == TType::STOP) {
        break;
      }
      switch ($fid)
      {
        case 1:
          if ($ftype == TType::I32) {
            $xfer += $input->readI32($this->type);
          } else {
            $xfer += $input->skip($ftype);
          }
          break;
        case 2:
          if ($ftype == TType::I64) {
            $xfer += $input->readI64($this->errorCode);
          } else {
            $xfer += $input->skip($ftype);
          }
          break;
        case 3:
          if ($ftype == TType::STRING) {
            $xfer += $input->readString($this->errorMessage);
          } else {
            $xfer += $input->skip($ftype);
          }
          break;
        case 4:
          if ($ftype == TType::STRING) {
            $xfer += $input->readString($this->debugMessage);
          } else {
            $xfer += $input->skip($ftype);
          }
          break;
        default:
          $xfer += $input->skip($ftype);
          break;
      }
      $xfer += $input->readFieldEnd();
    }
    $xfer += $input->readStructEnd();
    return $xfer;
  }

  public function write($output) {
    $xfer = 0;
    $xfer += $output->writeStructBegin('AirException');
    if ($this->type !== null) {
      $xfer += $output->writeFieldBegin('type', TType::I32, 1);
      $xfer += $output->writeI32($this->type);
      $xfer += $output->writeFieldEnd();
    }
    if ($this->errorCode !== null) {
      $xfer += $output->writeFieldBegin('errorCode', TType::I64, 2);
      $xfer += $output->writeI64($this->errorCode);
      $xfer += $output->writeFieldEnd();
    }
    if ($this->errorMessage !== null) {
      $xfer += $output->writeFieldBegin('errorMessage', TType::STRING, 3);
      $xfer += $output->writeString($this->errorMessage);
      $xfer += $output->writeFieldEnd();
    }
    if ($this->debugMessage !== null) {
      $xfer += $output->writeFieldBegin('debugMessage', TType::STRING, 4);
      $xfer += $output->writeString($this->debugMessage);
      $xfer += $output->writeFieldEnd();
    }
    $xfer += $output->writeFieldStop();
    $xfer += $output->writeStructEnd();
    return $xfer;
  }

}


