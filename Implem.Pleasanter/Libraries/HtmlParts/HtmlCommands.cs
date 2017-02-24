﻿using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using System;
namespace Implem.Pleasanter.Libraries.HtmlParts
{
    public static class HtmlCommands
    {
        public static HtmlBuilder MainCommands(
            this HtmlBuilder hb,
            long siteId,
            Permissions.Types pt,
            Versions.VerTypes verType,
            string referenceType = "",
            long referenceId = 0,
            bool backButton = true,
            bool updateButton = false,
            bool copyButton = false,
            bool moveButton = false,
            bool bulkMoveButton = false,
            bool mailButton = false,
            bool deleteButton = false,
            bool bulkDeleteButton = false,
            bool importButton = false,
            bool exportButton = false,
            Action extensions = null)
        {
            return hb.Div(id: "MainCommandsContainer", action: () => hb
                .Div(id: "MainCommands", action: () =>
                {
                    if (backButton)
                    {
                        hb.Button(
                            controlId: "GoBack",
                            text: Displays.GoBack(),
                            controlCss: "button-icon",
                            accessKey: "q",
                            onClick: "$p.back();",
                            icon: "ui-icon-circle-arrow-w");
                    }
                    if (pt.CanRead() && verType == Versions.VerTypes.Latest)
                    {
                        if (Routes.Action() == "new")
                        {
                            hb.Button(
                                text: Displays.Create(),
                                controlCss: "button-icon validate",
                                accessKey: "s",
                                onClick: "$p.create($(this));",
                                icon: "ui-icon-disk",
                                action: "Create",
                                method: "post");
                        }
                        else
                        {
                            hb
                                .Button(
                                    text: Displays.Update(),
                                    controlCss: "button-icon validate",
                                    accessKey: "s",
                                    onClick: "$p.send($(this));",
                                    icon: "ui-icon-disk",
                                    action: "Update",
                                    method: "put",
                                    _using: updateButton && pt.CanUpdate())
                                .Button(
                                    text: Displays.Copy(),
                                    controlCss: "button-icon open-dialog",
                                    accessKey: "c",
                                    onClick: "$p.openDialog($(this));",
                                    icon: "ui-icon-copy",
                                    selector: "#CopyDialog",
                                    _using: copyButton && pt.CanCreate())
                                .Button(
                                    text: Displays.Move(),
                                    controlCss: "button-icon open-dialog",
                                    accessKey: "o",
                                    onClick: "$p.moveTargets($(this));",
                                    icon: "ui-icon-transferthick-e-w",
                                    selector: "#MoveDialog",
                                    action: "MoveTargets",
                                    method: "get",
                                    _using: moveButton && pt.CanUpdate())
                                .Button(
                                    text: Displays.BulkMove(),
                                    controlCss: "button-icon open-dialog",
                                    accessKey: "o",
                                    onClick: "$p.moveTargets($(this));",
                                    icon: "ui-icon-transferthick-e-w",
                                    selector: "#MoveDialog",
                                    action: "MoveTargets",
                                    method: "get",
                                    _using: bulkMoveButton && pt.CanUpdate())
                                .Button(
                                    controlId: "EditOutgoingMail",
                                    text: Displays.Mail(),
                                    controlCss: "button-icon",
                                    onClick: "$p.openOutgoingMailDialog($(this));",
                                    icon: "ui-icon-mail-closed",
                                    action: "Edit",
                                    method: "put",
                                    accessKey: "m",
                                    _using: mailButton && pt.CanSendMail())
                                .Button(
                                    text: Displays.Delete(),
                                    controlCss: "button-icon",
                                    accessKey: "r",
                                    onClick: "$p.send($(this));",
                                    icon: "ui-icon-trash",
                                    action: "Delete",
                                    method: "delete",
                                    confirm: "ConfirmDelete",
                                    _using: deleteButton && pt.CanDelete())
                                .Button(
                                    text: Displays.BulkDelete(),
                                    controlCss: "button-icon",
                                    accessKey: "r",
                                    onClick: "$p.send($(this));",
                                    icon: "ui-icon-trash",
                                    action: "BulkDelete",
                                    method: "delete",
                                    confirm: "ConfirmDelete",
                                    _using: bulkDeleteButton && pt.CanDelete())
                                .Button(
                                    controlId: "EditImportSettings",
                                    text: Displays.Import(),
                                    controlCss: "button-icon",
                                    accessKey: "w",
                                    onClick: "$p.openImportSettingsDialog($(this));",
                                    icon: "ui-icon-arrowreturnthick-1-e",
                                    selector: "#ImportSettingsDialog",
                                    _using: importButton && pt.CanImport())
                                .Button(
                                    controlId: "EditExportSettings",
                                    text: Displays.Export(),
                                    controlCss: "button-icon",
                                    accessKey: "x",
                                    onClick: "$p.openExportSettingsDialog($(this));",
                                    icon: "ui-icon-arrowreturnthick-1-w",
                                    action: Locations.ItemAction(
                                        siteId, "ExportSettings", "Edit"),
                                    method: "put",
                                    _using: exportButton && pt.CanExport());
                        }
                    }
                    extensions?.Invoke();
                }));
        }
    }
}