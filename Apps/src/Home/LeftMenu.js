import React from 'react';
import { Menu } from 'antd';
const { SubMenu } = Menu;
import { RiseOutlined,  BarChartOutlined,  TeamOutlined,    ZoomInOutlined, ShoppingCartOutlined, PullRequestOutlined, IdcardOutlined, RetweetOutlined, DatabaseOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';



const LeftMenu = () => {

	return (
		<Menu mode="inline"
			theme="dark"
			style={{ height: '100vh', borderRight: 0 }}>

			<SubMenu key="sub1" icon={<RetweetOutlined />} title={'Рабочая область'} >
				<Menu.Item key="1" icon={<PullRequestOutlined />}><Link to="/Requests">Заявки</Link></Menu.Item>
			</SubMenu>

			<SubMenu key="sub2" icon={<DatabaseOutlined />} title={'Справочники'} >

				<Menu.Item key="2" icon={<IdcardOutlined />}><Link to="/Clients">Клиенты</Link></Menu.Item>
				<Menu.Item key="3" icon={<TeamOutlined />}><Link to="/Users">Пользователи</Link></Menu.Item>
				<Menu.Item key="4" icon={<ShoppingCartOutlined />}><Link to="/Trailers">Прицепы</Link></Menu.Item>

			</SubMenu>

			<SubMenu key="sub3" icon={<ZoomInOutlined />} title={'Аналитика'}>
				<Menu.Item key="6" icon={<BarChartOutlined />}><Link to="/Reports">Отчеты</Link></Menu.Item>
				<Menu.Item key="7" icon={<RiseOutlined />} ><Link to="/Events">События</Link></Menu.Item>
			</SubMenu>

		</Menu >
	)

}
export default LeftMenu