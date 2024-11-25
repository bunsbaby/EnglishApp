import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, Upload } from 'antd';
import DocumentInsertDto from './models/DocumentInsertDto';
import { UploadOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import type { UploadFile } from 'antd/es/upload/interface';
import axios from '../../common/baseAxios';
interface IAddCourseProps {
    open: boolean,
    closeForm: Function,
}
const AddModal: React.FC<IAddCourseProps> = (props: IAddCourseProps) => {
    const { open, closeForm } = props;
    const [fileList, setFileList] = useState<UploadFile[]>([]);
    const onFinish = (input: DocumentInsertDto) => {
        if(fileList && fileList.length > 0) {
            input.displayName = fileList[0].name;
            input.fileName = fileList[0].response?.fileName;
            input.documentSize = fileList[0].size;
        }
        axios.post(`Documents`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Tạo tài liệu thành công.')
                closeForm(true);
            }
        })
    }
    const propsUpload: UploadProps = {
        name: 'file',
        action: `http://localhost:5000/api/Documents/Upload`,
        headers: {
          authorization: '',
        },
        onChange(info) {
            setFileList(info.fileList);
        },
      };
    return (
        <>
            <Modal
                title="THÊM TÀI LIỆU"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={1000}
            >
                <Form
                    name="basic"
                    layout={'vertical'}
                    style={{ maxWidth: 1000 }}
                    onFinish={onFinish}
                    autoComplete="off"
                >
                     <Form.Item label="Tên tài liệu" name="name" rules={[{ required: true, message: 'Vui lòng nhập tên tài liệu!' }]}>
                        <Input placeholder='Nhập tên tài liệu' />
                    </Form.Item>
                    <Form.Item>
                        <Upload {...propsUpload} maxCount={1}>
                            <Button icon={<UploadOutlined />}>Tải tài liệu</Button>
                        </Upload>
                    </Form.Item>
                    <Form.Item label="Mô tả" name="description">
                        <Input.TextArea placeholder='Mô tả' />
                    </Form.Item>
                    <Form.Item wrapperCol={{ span: 24 }} style={{ textAlign: 'right' }}>
                        <Button type="primary" htmlType="submit" >
                            Lưu
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AddModal;
