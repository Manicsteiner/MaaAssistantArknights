#pragma once

#include "AdbController.h"
#include "ControllerAPI.h"
#include "MaatouchController.h"
#include "MinitouchController.h"
#include "PlayToolsController.h"
#include "WSAController.h"

namespace asst
{
    class ControllerFactory
    {
    public:
        ControllerFactory(const AsstCallback& callback, Assistant* inst) : m_callback(callback), m_inst(inst) {}
        ~ControllerFactory() = default;

        std::shared_ptr<ControllerAPI> create_controller(ControllerType type, PlatformType platform_type)
        {
            std::shared_ptr<ControllerAPI> controller;
            try {
                switch (type) {
                case ControllerType::Adb:
                    controller = std::make_shared<AdbController>(m_callback, m_inst, platform_type);
                    break;
                case ControllerType::Minitouch:
                    controller = std::make_shared<MinitouchController>(m_callback, m_inst, platform_type);
                    break;
                case ControllerType::Maatouch:
                    controller = std::make_shared<MaatouchController>(m_callback, m_inst, platform_type);
                    break;
                case ControllerType::MacPlayTools:
                    controller = std::make_shared<PlayToolsController>(m_callback, m_inst, platform_type);
                    break;
                case ControllerType::WSA:
                    controller = std::make_shared<WSAController>(m_callback, m_inst, platform_type);
                    break;
                default:
                    return nullptr;
                }
            }
            catch (const std::exception& e) {
                Log.error("Cannot create controller: {}", e.what());
                return nullptr;
            }
            return controller;
        }

    private:
        AsstCallback m_callback;
        Assistant* m_inst;
    };
}
