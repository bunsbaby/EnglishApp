import React, { useEffect, useState } from 'react';
import { Button, Modal, Form, Input, Select, message, DatePicker } from 'antd';
import ClassInsertDto from './models/ClassInsertDto';
import axios from '../../common/baseAxios';
import TeacherDto from 'pages/Teacher/models/TeacherDto';
import ILessonDto from 'pages/Class/models/LessonDto';
interface IAddClassProps {
    open: boolean,
    closeForm: Function,
}
interface IOptions {
    value: Number,
    label: String
};
const AddModal: React.FC<IAddClassProps> = (props: IAddClassProps) => {
    const { open, closeForm } = props;
    const [teachersOptions, setTeachers]  = useState<any>(Array<IOptions>);
    const [lessonOptions, setLessons] = useState<any>(Array<IOptions>);
    const [courseOptions, setCourses] = useState<any>(Array<IOptions>);
    useEffect(() => {
        getTeachers();
        getLessons();
        getCourses();
    }, [])
    const getTeachers = () => {
        axios.get(`Teachers`).then((res) => {
            if (res.data.status) {
                let teachers: Array<TeacherDto> = res.data.data;
                let teacherOptions: Array<IOptions> = new Array<IOptions>();
                teachers.forEach((m) => {
                    let option: IOptions =  {
                        value: m.id,
                        label: m.name
                    };
                    teacherOptions.push(option);
                })
                setTeachers(teacherOptions);
            }
        })
    }
    const getLessons = () => {
        axios.get(`Classes/GetLessons`).then((res) => {
            if(res.data.status) {
                let lessons: Array<ILessonDto> = res.data.data;
                let options: Array<IOptions> = new Array<IOptions>();
                lessons.forEach((m) => {
                    let option: IOptions = {
                        label: m.name,
                        value: m.id
                    };
                    options.push(option);
                })
                setLessons(options);
            }
        })
    }
    const getCourses = () => {
        axios.get(`Courses`).then((res) => {
            if(res.data.status) {
                let courses: Array<ILessonDto> = res.data.data;
                let options: Array<IOptions> = new Array<IOptions>();
                courses.forEach((m) => {
                    let option: IOptions = {
                        label: m.name,
                        value: m.id
                    };
                    options.push(option);
                })
                setCourses(options);
            }
        })
    }
    const onFinish = (input: ClassInsertDto) => {
        axios.post(`Classes`, input).then((res) => {
            if (res?.data.status === true) {
                message.success('Tạo lớp học thành công.')
                closeForm(true);
            }
        })
    }
    return (
        <>
            <Modal
                title="THÊM LỚP HỌC"
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
                    <Form.Item label="Lớp học" name="name" rules={[{ required: true, message: 'Vui lòng nhập lớp học!' }]}>
                        <Input placeholder='Tên lớp học' />
                    </Form.Item>
                    <Form.Item label="Mô tả lớp học" name="description" rules={[{ message: 'Vui lòng nhập mô tả!' }]}>
                        <Input placeholder='Mô tả lớp học' />
                    </Form.Item>
                    <Form.Item label="Khóa học" name="courseId" rules={[{ required: true, message: 'Vui lòng chọn khóa học!' }]}>
                        <Select
                            placeholder="Chọn khóa học"
                            allowClear
                            options={courseOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item name="teacherId" label="Giảng viên" rules={[{ required: true, message: 'Vui lòng chọn giảng viên !' }]}>
                        <Select
                            placeholder="Chọn giảng viên"
                            allowClear
                            options={teachersOptions}
                        >
                        </Select>
                    </Form.Item>
                    <Form.Item name="lessonId" label="Buổi học" rules={[{ required: true, message: 'Vui lòng chọn buổi học !' }]}>
                        <Select
                            placeholder="Chọn buổi học"
                            allowClear
                            options={lessonOptions}
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
