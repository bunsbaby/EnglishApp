import React, { useEffect, useState } from "react";
import { Button, Col, Row, Input, Space, Table, Tag } from "antd";
import { SearchOutlined, InfoCircleOutlined, FormOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import ClassDto from 'pages/Class/models/ClassDto';
import axios from '../../common/baseAxios';
import ViewModal from "./View";
import AddModal from "./Add";
import EditModal from "./Edit";
const Class = () => {
    const [data, setData] = useState(new Array<ClassDto>);
    const [open, setOpen] = useState(false);
    const [openView, setOpenView] = useState(false);
    const [openEdit, setOpenEdit] = useState(false);
    const [curentId, setCurentId] = useState<any>(Number);
    const [curentClass, setCurentClass] = useState<any>();
    const columns: ColumnsType<ClassDto> = [
        {
            title: 'Lớp Học',
            dataIndex: 'name',
            key: 'name'
        },
        {
            title: 'Khóa Học',
            dataIndex: 'courseName',
            key: 'courseName'
        },
        {
            title: 'Giảng Viên',
            dataIndex: 'teacherName',
            key: 'teacherName'
        },
        {
            title: 'Sĩ số',
            dataIndex: 'studentCount',
            key: 'studentCount'
        },
        {
            title: 'Buổi Học',
            dataIndex: 'lessonName',
            key: 'lessonName'
        },
        {
            title: ``,
            key: `action`,
            render: (record) => (
                <Space size="middle">
                    <Button size="middle" onClick={() => handleFormView(record.id)}><InfoCircleOutlined /></Button>
                    <Button size="middle" onClick={() => handleFormEdit(record.id)}><FormOutlined /></Button>
                </Space>
            )
        }
    ]
    useEffect(() => {
        getListClasses();
    }, [])
    const getListClasses = (search: string = '') => {
        axios.get(`Classes?search=${search}`).then((res) => {
            if (res?.data?.status) {
                setData(res.data.data);
            }
        })
    }
    const closeForm = (isSave = false) => {
        setOpen(false);
        setOpenView(false);
        setOpenEdit(false);
        if (isSave) {
            getListClasses();
        }
    }
    const handleFormView = (id: any) => {
        setCurentId(id);
        setOpenView(true);
    }
    const handleFormEdit = (id: any) => {
        axios.get(`Classes/${id}`).then((res) => {
            setCurentClass(res.data.data);
            setOpenEdit(true);
        })
    }
    const handleOnChange = (event: any) => {
        getListClasses(event.target.value);
    }
    return <>
        <div>
            <Row>
                <Col span={24} style={{ fontWeight: 700, fontSize: '23px' }}>QUẢN LÝ LỚP HỌC</Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={12}>
                    <Input onChange={handleOnChange} placeholder="Tìm kiếm theo tên lớp" prefix={<SearchOutlined />} />
                </Col>
                <Col span={12} style={{ textAlign: 'right' }}>
                    <Button onClick={() => setOpen(true)}>Thêm Mới</Button>
                </Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} rowKey="id" ></Table>
                </Col>
            </Row>
        </div>
        {open && <AddModal open={open} closeForm={closeForm} />}
        {openView && <ViewModal open={openView} closeForm={closeForm} id={curentId} />}
        {openEdit && <EditModal open={openEdit} closeForm={closeForm} curentClass={curentClass} />}
    </>
}
export default Class;