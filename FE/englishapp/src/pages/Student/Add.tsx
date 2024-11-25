import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message } from 'antd';
import axios from '../../common/baseAxios';
import CourseDto from 'pages/Course/models/CourseDto';
import StudentInsertDto from './models/StudentInsertDto';
interface IAddStudentProps {
    open: boolean,
    closeForm: Function,
}
interface IClassOptions {
    value: Number,
    label: String
};
const AddModal: React.FC<IAddStudentProps> = (props: IAddStudentProps) => {
    const { open, closeForm } = props;
    const [classOptions, setClassOptions]  = useState<any>(Array<IClassOptions>);
    const onFinish = (input: StudentInsertDto) => {
        axios.post(`Students`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Tạo học viên thành công.')
                closeForm(true);
            }
        })
    }
    useEffect(() => {
        getCourse();
    }, []);
    const getCourse = () => {
        axios.get(`Courses`).then((res) => {
            if (res.data.status) {
                let courses: Array<CourseDto> = res.data.data;
                let courseOptions: Array<IClassOptions> = new Array<IClassOptions>();
                courses.forEach((m) => {
                    let option: IClassOptions =  {
                        value: m.id,
                        label: m.name
                    };
                    courseOptions.push(option);
                })
                setClassOptions(courseOptions);
            }
        })
    }
    return (
        <>
            <Modal
                title="THÊM HỌC VIÊN"
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
                    <Form.Item label="Họ & Tên" name="name" rules={[{ required: true, message: 'Vui lòng nhập họ & tên của học viên!' }]}>
                        <Input placeholder='Họ & Tên của học viên' />
                    </Form.Item>
                    <Form.Item name="genderId" label="Giới Tính" rules={[{ required: true, message: 'Vui lòng chọn giới tính học viên !' }]}>
                        <Select
                            placeholder="Chọn giới tính học viên"
                            allowClear
                        >
                            <Select.Option value="1">Nam</Select.Option>
                            <Select.Option value="2">Nữ</Select.Option>
                            <Select.Option value="3">Khác</Select.Option>
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ E-mail"
                        name="email"
                        rules={[
                            { required: true, message: 'Vui lòng nhập địa chỉ E-mail của học viên!' },
                            { type: 'email', message: 'Định dạng E-mail không đúng!' }
                        ]}
                    >
                        <Input placeholder='Địa chỉ E-mail của học viên' />
                    </Form.Item>
                    <Form.Item
                        label="Số Điện Thoại"
                        name="phone"
                        rules={[{ required: true, message: 'Vui lòng nhập số điện thoại của học viên!' }]}
                    >
                        <Input placeholder='Số điện thoại của học viên' />
                    </Form.Item>
                    <Form.Item
                        label="Địa Chỉ"
                        name="address"
                        rules={[{ required: false }]}
                    >
                        <Input placeholder='Địa chỉ của học viên' />
                    </Form.Item>
                    <Form.Item name="classId" label="Lớp">
                        <Select
                            placeholder="Chọn lớp học viên"
                            allowClear
                            options={classOptions}
                        >
                        </Select>
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
