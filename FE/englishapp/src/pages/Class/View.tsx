import React, { useEffect, useState } from 'react';
import { Button, Col, Modal, Row, Space, message } from 'antd';
import DefaultAvatar from 'access/images/avatar.png';
import axios from '../../common/baseAxios';
import ClassDto from './models/ClassDto';
import moment from 'moment';
interface IAddClassProps {
    open: boolean,
    closeForm: Function,
    id: Number
}
const ViewModal: React.FC<IAddClassProps> = (props: IAddClassProps) => {
    const { open, closeForm, id } = props;
    const [curentClass, setCurentClass] = useState<ClassDto>();

    useEffect(() => {
        getClassById();
    }, [])
    const getClassById = () => {
        axios.get(`Classes/${id}`).then((res) => {
            setCurentClass(res.data.data);
        })
    }
    const handleDelete = () => {
        axios.delete(`Classes/${id}`).then((res) => {
            if(res?.data?.status) {
                message.success('Xóa lớp học thành công.')
                closeForm(true);
            }
            else {
                message.success('Xóa lớp học thất bại.')
            }
        })
    }
    return (
        <>
            <Modal
                title="CHI TIẾT LỚP HỌC"
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
                            <Col span={6}>Mã ID:</Col>
                            <Col span={18}>{curentClass?.id?.toString()}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>
                                Tên lớp học
                            </Col>
                            <Col span={18}>{curentClass?.name}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>
                                Mô tả
                            </Col>
                            <Col span={18}>{curentClass?.description}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Khóa học</Col>
                            <Col span={18}>{curentClass?.courseName}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Giảng viên:</Col>
                            <Col span={18}>{curentClass?.teacherName}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Sĩ số:</Col>
                            <Col span={18}>{curentClass?.studentCount}</Col>
                        </Row>
                        <Row style={{ marginTop: '15px' }}>
                            <Col span={6}>Buổi Học:</Col>
                            <Col span={18}>
                                {curentClass?.lessonName}
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
