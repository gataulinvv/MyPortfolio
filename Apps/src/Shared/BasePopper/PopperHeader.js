import React from 'react';
import { observer } from 'mobx-react'
import styled from 'styled-components';
import themes from '/src/Shared/themes';
import { Button } from 'antd';
import { AppstoreAddOutlined } from '@ant-design/icons';
import { Edit } from '@styled-icons/fluentui-system-regular';

const EditIcon = styled(Edit)`
height: 1.5em;
`;


const PopperHeader = (props) => {

	const { titleType, showcase, onOpen } = props;

	var theme = themes["male"];

	var content = '';

	switch (titleType) {
		case "add":
			content =
				<Button style={{ background: theme.background, color: theme.color }} icon={<AppstoreAddOutlined />} onClick={(event) => onOpen(event.currentTarget)}>
					{showcase}
				</Button >

			break;
		case "edit":
			content =
				<Button type="link" icon={<EditIcon />} onClick={(event) => onOpen(event.currentTarget)}></Button>
			break;
		default:
			content = <Button onClick={(event) => onOpen(event.currentTarget)}> {showcase}
			</Button >
	}

	return ( content )
}

export default observer(PopperHeader);

