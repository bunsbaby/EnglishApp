import React, { useEffect, useState } from 'react';
import { Button, Col, Modal, Row, Space, message } from 'antd';
import DefaultAvatar from 'access/images/avatar.png';
import axios from '../../common/baseAxios';
import CourseDto from './models/CourseDto';
import moment from 'moment';
interface IAddCourseProps {
    open: boolean,
    closeForm: Function,
    id: Number
}
const ViewModal: React.FC<IAddCourseProps> = (props: IAddCourseProps) => {
    const { open, closeForm, id } = props;
    const [curentCourse, setCurentCourse] = useState<CourseDto>();

    useEffect(() => {
        getCourseById();
    }, [])
    const getCourseById = () => {
        axios.get(`Courses/${id}`).then((res) => {
            setCurentCourse(res.data.data);
        })
    }
    const handleDelete = () => {
        axios.delete(`Courses/${id}`).then((res) => {
            if(res?.data?.status) {
                message.success('Xóa khóa học thành công.')
                closeForm(true);
            }
            else {
                message.success('Xóa khóa học thất bại.')
            }
        })
    }
    return (
        <>
            <Modal
                title="CHI TIẾT KHÓA HỌC"
                centered
                open={open}
                onOk={() => closeForm(false)}
                onCancel={() => closeForm(false)}
                footer={null}
                width={700}
            >
                <Row>
                    <Col span={24}>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Mã ID</Col>
                            <Col span={18}>{curentCourse?.id?.toString()}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>
                                Tên khóa học
                            </Col>
                            <Col span={18}>{curentCourse?.name}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>
                                Mô tả
                            </Col>
                            <Col span={18}>{curentCourse?.description}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Thời Gian Bắt Đầu</Col>
                            <Col span={18}>
                                {moment(curentCourse?.startDated).format("DD-MM-YYYY")}
                            </Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Gói Khóa Học</Col>
                            <Col span={18}>
                                {curentCourse?.packageType + ` Tháng`}
                            </Col>
                        </Row>
                    </Col>
                </Row>
                <Row style={{ marginTop: '15px', textAlign: 'right' }}>
                    <Col span={24}>
                        <Button type="primary" danger onClick={() => handleDelete()}>Xóa</Button>
                    </Col>
                </Row>
            </Modal>
        </>
    );
};

export default ViewModal;
