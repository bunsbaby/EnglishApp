import React, { useEffect, useState } from "react";
import { Button, Col, Row, Input, Space, Table, Tag } from "antd";
import { SearchOutlined, InfoCircleOutlined, FormOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import TeacherDto from 'pages/Teacher/models/TeacherDto';
import AddModal from "./Add";
import ViewModal from "./View";
import EditModal from "./Edit";
import axios from '../../common/baseAxios';
const Teacher = () => {
    const [open, setOpen] = useState(false);
    const [openView, setOpenView] = useState(false);
    const [openEdit, setOpenEdit] = useState(false);
    const [curentId, setCurentId] = useState<any>(Number);
    const [curentTeacher, setCurentTeacher] = useState<any>();
    const [data, setData] = useState(new Array<TeacherDto>);
    const closeForm = (isSave = false) => {
        setOpen(false);
        setOpenView(false);
        setOpenEdit(false);
        if (isSave) {
            getListTeachers();
        }
    }
    useEffect(() => {
        getListTeachers();
    }, [])
    const columns: ColumnsType<TeacherDto> = [
        {
            title: 'Họ & Tên',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Địa Chỉ E-mail',
            dataIndex: 'email',
            key: 'email'
        },
        {
            title: 'Giới Tính',
            key: 'genderId',
            dataIndex: 'genderId',
            render: (genderId) => (
                <Space size="middle">
                    {genderId === 1 ? 'Nam' : (genderId === 2 ? 'Nữ' : 'Khác')}
                </Space>
            )
        },
        {
            title: 'Số Điện Thoại',
            dataIndex: 'phone',
            key: 'phone'
        },
        {
            title: ``,
            key: `action`,
            dataIndex: 'id',
            render: (id) => (
                <Space size="middle">
                    <Button size="middle" onClick={() => handleFormView(id)}><InfoCircleOutlined /></Button>
                    <Button size="middle" onClick={() => handleFormEdit(id)}><FormOutlined /></Button>
                </Space>
            )
        }
    ]
    const getListTeachers = (search: string = '') => {
        axios.get(`Teachers?search=${search}`).then((res) => {
            if (res?.data?.status) {
                setData(res.data.data);
            }
        })
    }
    const handleFormView = (id: Number) => {
        setCurentId(id);
        setOpenView(true);
    }
    const handleOnChange = (event: any) => {
        getListTeachers(event.target.value);
    }
    const handleFormEdit = (id: number) => {
        axios.get(`Teachers/${id}`).then((res) => {
            setCurentTeacher(res.data.data);
            setOpenEdit(true);
        });
    }
    return <>
        <div>
            <Row>
                <Col span={24} style={{ fontWeight: 700, fontSize: '23px' }}>QUẢN LÝ GIẢNG VIÊN</Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={12}>
                    <Input onChange={handleOnChange} placeholder="Tìm kiếm theo tên hoặc email" prefix={<SearchOutlined />} />
                </Col>
                <Col span={12} style={{ textAlign: 'right' }}>
                    <Button onClick={() => setOpen(true)}>Thêm Mới</Button>
                </Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} rowKey="id"></Table>
                </Col>
            </Row>
        </div>

        {open && <AddModal open={open} closeForm={closeForm} />}
        {openView && <ViewModal open={openView} closeForm={closeForm} id={curentId} />}
        {openEdit && <EditModal open={openEdit} closeForm={closeForm} curentTeacher={curentTeacher} />}
    </>
}
export default Teacher;