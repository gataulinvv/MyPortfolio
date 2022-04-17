import { observer } from 'mobx-react';
import React, { useState, useEffect } from 'react';
import { Layout, Menu } from 'antd';
import {
  GlobalOutlined, DesktopOutlined, HomeOutlined, MenuUnfoldOutlined, MenuFoldOutlined,
} from '@ant-design/icons';
import {
  BrowserRouter as Router, Route, Switch, Link,
} from 'react-router-dom';
import LeftMenu from './LeftMenu';
import MainLayoutStore from './MainLayoutStore';
import Clients from '../Catalogs/Clients/ClientsGrid';
import Users from '../Catalogs/Users/UsersGrid';
import Trailers from '../Catalogs/Trailers/TrailersGrid';
import Requests from '../Workspace/Requests/RequestsGrid';
import Reports from '../Analytics/Reports';
import Events from '../Analytics/Events';
import News from '../News';
import Invoices from '../Invoices/Invoices';
import ObjectInvoices from '../ObjectInvoices';
import CommonInfo from '../CommonInfo';
import UsersManagement from '../UsersManagement';
import SystemManagement from '../SystemManagement';
import NotFound from '../NotFound';

const {
  Header, Content, Sider, Footer,
} = Layout;

const SwitcherCollapse = () => {
  const Element = MainLayoutStore.collapsed ? MenuUnfoldOutlined : MenuFoldOutlined;

  return (
    <Element onClick={() => MainLayoutStore.SetCollapse()} />
  );
};

const MainLayout = () => (

  <Router>
    <Layout style={{ height: '100vh' }}>
      <Header>
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={['1']}
        >
          <Menu.Item key="0"><SwitcherCollapse /></Menu.Item>
          <Menu.Item key="1" icon={<HomeOutlined />}><Link to="/">Заявки</Link></Menu.Item>
          <Menu.Item key="2" icon={<GlobalOutlined />}><Link to="/News">Новости</Link></Menu.Item>
        </Menu>
      </Header>
      <Layout>
        <Sider
          trigger={null}
          collapsible
          collapsed={MainLayoutStore.collapsed}
        >
          <LeftMenu />

        </Sider>
        <Layout style={{ padding: '0 24px 24px' }}>

          <Content className="site-layout-background" style={{ padding: 24, margin: 0, minHeight: 500 }}>

            <Switch>

              <Route exact path="/"><Requests title="Заявки" /></Route>
              <Route path="/Requests"><Requests title="Заявки" /></Route>
              <Route path="/Clients"><Clients title="Клиенты" /></Route>
              <Route path="/Users"><Users title="Пользователи" /></Route>

              <Route path="/Invoices"><Invoices title="Начисления" /></Route>
              <Route path="/ObjectInvoices"><ObjectInvoices title="Зачисления" /></Route>
              <Route path="/Trailers"><Trailers title="Прицепы" /></Route>
              <Route path="/Reports"><Reports title="Отчеты" /></Route>
              <Route path="/Events"><Events title="События" /></Route>
              <Route path="/News"><News title="Новости" /></Route>
              <Route path="/CommonInfo"><CommonInfo title="Общая информация" /></Route>
              <Route path="/UsersManagement">
                <UsersManagement
                  title="Управление пользователями"
                />
              </Route>
              <Route path="/SystemManagement"><SystemManagement title="Управление системой" /></Route>
              <Route path="*"><NotFound title="Страница не найдена, или доступ запрещен" /></Route>
            </Switch>
          </Content>

        </Layout>
      </Layout>
    </Layout>
  </Router>

);

export default observer(MainLayout);
