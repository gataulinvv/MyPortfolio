/* eslint-disable no-tabs */
/* eslint-disable no-mixed-spaces-and-tabs */
import React, { useState } from 'react';
import { observer } from 'mobx-react';
import Popper from '@material-ui/core/Popper';
import MuiDialogTitle from '@material-ui/core/DialogTitle';
import Typography from '@material-ui/core/Typography';
import Draggable from 'react-draggable';
import { Button } from 'antd';
import { CloseOutlined, MinusSquareOutlined, FullscreenOutlined } from '@ant-design/icons';

import themes from '/src/Shared/themes';

import BasePopperStore from './BasePopperStore';
import PopperHeader from './PopperHeader';

const stylesStore = () => ({

  closeButton: {
    position: 'absolute',
    right: '0px',
    top: '0px',
  },

  minMaxButton: {
    position: 'absolute',
    right: '35px',
    top: '0px',
  },

});

const DialogTitle = (props) => {
  const {
    theme, store, children, styles, onMinMax, onClose,
  } = props;

  return (
    <MuiDialogTitle disableTypography style={{ background: theme.background, color: theme.color }}>
      <Typography>{children}</Typography>

      {!store.min ? (
        <Button
          icon={<MinusSquareOutlined />}
          style={styles.minMaxButton}
          onClick={() => onMinMax(true)}
        />
      ) : null}

      {store.min ? (
        <Button
          icon={<FullscreenOutlined />}
          style={styles.minMaxButton}
          onClick={() => onMinMax(false)}
        />
      ) : null}

      {onClose ? (
        <Button icon={<CloseOutlined />} style={styles.closeButton} onClick={() => onClose(false)} />
      ) : null}

    </MuiDialogTitle>
  );
};

const styles = stylesStore();

const BasePopper = ({
  titleType, header, showcase, Content,
}) => {
  const [basePopperStore] = useState(new BasePopperStore());

  const hendleMinMax = (min) => {
    basePopperStore.Minimize(min);
  };

  const handleDragStop = (event) => {
    basePopperStore.HideShowComponents();
  };

  const handleOpenClose = (targer) => {
    basePopperStore.SetAnchor(targer);
  };
  const theme = themes.male;

  return (

    <div>

      <PopperHeader titleType={titleType} showcase={showcase} onOpen={handleOpenClose} />

      {basePopperStore.open ? (

        <Popper
          style={{ visibility: basePopperStore.hidden, zIndex: 1000 }}
          open={basePopperStore.open}
          anchorEl={basePopperStore.anchor}
          placement="left-start"
        >

          <Draggable onStop={(event) => handleDragStop(event)}>

            <div style={{
              background: 'white',
              border: '2px solid #000',
              padding: '2px',
              visibility: basePopperStore.visible,
            }}
            >

              <DialogTitle
                theme={theme}
                styles={styles}
                store={basePopperStore}
                onMinMax={hendleMinMax}
                onClose={() => handleOpenClose(null)}
              >
                {`${header} - ${showcase}`}
              </DialogTitle>

              <div
                style={{
								  height: !basePopperStore.min ? null : '0px',
								  visibility: !basePopperStore.min ? 'visible' : 'hidden',
                }}
              >
                {' '}
                {Content}
              </div>

            </div>
          </Draggable>

        </Popper>
      ) : null}
    </div>
  );
};

export default observer(BasePopper);
